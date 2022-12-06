using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class Board : MonoBehaviour
{
    public int x_widht;
    public int y_height;
    public int offSet;
    public GameObject tilePrefarb;
    public GameObject[] candies;
    public Tile[,] allTiles;
    public GameObject[,] allCandies;

    public Camera cam;
    private int candyToUse;

    void Start()
    {
        allTiles = new Tile[x_widht, y_height];
        allCandies = new GameObject[x_widht, y_height];
        SetUp();
    }

    private void SetUp()
    {
        for (int i = 0; i < x_widht; i++)
        {
            for (int j = 0; j < y_height; j++)
            {
                Vector2 spawnPosition = new Vector2(i, j + offSet);
                GameObject tile = SpawnTile(spawnPosition);
                GameObject candy = SpawnCandy(spawnPosition);
                allCandies[i, j] = candy;
                int maxIterations = 0;
                while(MatchesAt(i,j, candies[candyToUse]) && maxIterations < 100)
                {
                    candyToUse = Random.Range(0,candies.Length);
                    maxIterations++;
                    Debug.Log(maxIterations);
                }
                maxIterations = 0;
                candy.GetComponent<Candy>().row = j;
                candy.GetComponent<Candy>().column = i;
            }
        }
    }

    private GameObject SpawnTile(Vector2 tilePosition)
    {
        GameObject tile = Instantiate(tilePrefarb, tilePosition, Quaternion.identity);
        tile.transform.SetParent(transform);
        tile.name = $"(Tile {tilePosition.x}, {tilePosition.y} )";
        return tile;
    }

    private GameObject SpawnCandy(Vector2 candyPosition)
    {
        int candyToUse = Random.Range(0, candies.Length);
        GameObject candy = Instantiate(candies[candyToUse], candyPosition, Quaternion.identity);
        candy.transform.SetParent(transform);
        candy.GetComponent<Candy>().Init(cam, this);
        candy.name = $"(Candy {candyPosition.x}, {candyPosition.y} )";
        return candy;
    }
    private bool MatchesAt( int column, int row, GameObject piece)
    {
        if(column > 1 && row > 1) 
        {
            if (allCandies[column -1, row].tag == piece.tag && allCandies[column -2, row].tag == piece.tag)
            {
                return true;
            }
            if (allCandies[column, row -1].tag == piece.tag && allCandies[column, row -2].tag == piece.tag)
            {
                return true;
            }
        }
        else if (column <= 1 || row <= 1)
        {
            if (row > 1)
            {
                if (allCandies[column, row -1].tag == piece.tag && allCandies[column, row -2].tag == piece.tag)
                {
                    return true;
                }
            }
            if (column > 1)
            {
                if (allCandies[column -1, row].tag == piece.tag && allCandies[column -2, row].tag == piece.tag)
                {
                    return true;
                }
            }
        }
        return false;
    }
    private void DestroyMatchesAt(int column, int row)
    {
        if(allCandies[column, row].GetComponent<Candy>().isMatched)
        {
            Destroy(allCandies[column, row]);
            allCandies[column, row] = null;
        }
    }

    public void DestroyMatches()
    {
        for (int i = 0; i < x_widht; i++)
        {
            for (int j = 0;  j< y_height; j++)
            {
                if (allCandies[i, j] != null)
                {
                    DestroyMatchesAt(i, j);
                }
            }
        }
        StartCoroutine(DecreaseRowCo());
    }

    private IEnumerator DecreaseRowCo()
    {
        int nullCount = 0;
        for (int i = 0; i < x_widht; i++)
        {
            for (int j = 0; j < y_height; j++)
            {
                if (allCandies[i,j] == null)
                {
                    nullCount++;
                }
                else if(nullCount > 0)
                {
                    allCandies[i, j].GetComponent<Candy>().row -= nullCount;
                    allCandies[i, j] = null;
                }
            }
            nullCount = 0;
        }
        yield return new WaitForSeconds(.4f);
        StartCoroutine(FillBoardCo());
    } 
    private void RefillBoard()
    {
        for (int i = 0; i < x_widht; i++)
        {
            for (int j = 0; j < y_height; j++)
            {
                if (allCandies[i,j] == null)
                {
                    Vector2 tempPosition = new Vector2(i, j + offSet);
                    int dotToUse = Random.Range(0, candies.Length);
                    GameObject piece = Instantiate(candies[candyToUse], tempPosition, Quaternion.identity);
                    allCandies[i, j] = piece;
                    piece.GetComponent<Candy>().row = j;
                    piece.GetComponent<Candy>().column = i;
                }
            }
        }
    }

    private bool MatchesOnBoard()
    {
        for (int i = 0; i < x_widht; i++)
        {
            for(int j = 0; j < y_height; i++)
            {
                if (allCandies[i,j]!= null)
                {
                    if (allCandies[i, j].GetComponent<Candy>().isMatched)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }

    private IEnumerator FillBoardCo()
    {
        RefillBoard();
        yield return new WaitForSeconds(.5f);

        while (MatchesOnBoard())
        {
            yield return new WaitForSeconds(.5f);
            DestroyMatches();
        }
    }
}
