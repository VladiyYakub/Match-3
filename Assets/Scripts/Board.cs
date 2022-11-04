using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public int x_width;

    public int y_height;

    public GameObject tilePrefab;

    private Tile[,] allTiles;

    void Start()
    {
        allTiles = new Tile[x_width, y_height];
        SetUp();
    }

    private void SetUp()
    {
        for (int i = 0; i < x_width; i++)
        {
            for (int j = 0; j < y_height; j++)
            {
                Vector2 tempPosition = new Vector2(i, j);

                GameObject backgroundTile = Instantiate(tilePrefab, tempPosition, Quaternion.identity) as GameObject;
                backgroundTile.transform.parent = transform;
                backgroundTile.name = "( " + i + ", " + j + " )";

                GameObject bachgroundTile = Instantiate(tilePrefab, tempPosition, Quaternion.identity) as GameObject;
                bachgroundTile.transform.parent = transform;
                bachgroundTile.name = "( " + i + ", " + j + " )";

            }
        }
    }

}
