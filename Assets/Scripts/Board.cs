using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Board : MonoBehaviour
{
    public int x_widht;
    public int y_height;
    public GameObject tilePrefarb;
    public GameObject[] candies;
    private Tile[,] allTiles;
    public GameObject[,] allCandies;
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
                GameObject tile = Instantiate(tilePrefarb, tempPosition, Quaternion.identity) as GameObject;
                tile.transform.parent = transform;
                tile.name = "( " + i + ", " + j + " )";
                int cnadyToUse = Random.Range(0, candies.Length);
                GameObject candy = Instantiate(candies[cnadyToUse], tempPosition, Quaternion.identity);
                candy.transform.parent = transform;
                candy.name = "( " + i + ", " + j + " )";
                allCandies[i,j] = candy;


            }
        }
    }
}
