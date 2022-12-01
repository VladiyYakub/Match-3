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
                Vector2 tempPosition = new Vector2(i, j);                
                GameObject tile = Instantiate(tilePrefarb, tempPosition, Quaternion.identity, transform);                
                tile.transform.parent = this.transform;                
                tile.name = $"( {i}, {j} )";
                int candyToUse = Random.Range(0, candies.Length);
                GameObject candy = Instantiate(candies[candyToUse], tempPosition, Quaternion.identity);
                candy.GetComponent<Candy>().Init(cam);                               
                candy.transform.parent = this.transform;
                candy.name = $"( {i}, {j} )";
                allCandies[i, j] = candy;              

            }
        }
    }
}
