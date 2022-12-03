using UnityEngine;

public class Board : MonoBehaviour
{
    public int x_widht;
    public int y_height;
    public GameObject tilePrefarb;
    public GameObject[] candies;
    public Tile[,] allTiles;
    public GameObject[,] allCandies;
    public Camera cam;


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
                Vector2 spawnPosition = new Vector2(i, j);

                GameObject tile = SpawnTile(spawnPosition);
                GameObject candy = SpawnCandy(spawnPosition);

                allCandies[i, j] = candy;
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
}
