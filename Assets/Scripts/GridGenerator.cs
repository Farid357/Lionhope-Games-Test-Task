using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridGenerator : MonoBehaviour
{
    [SerializeField] private Cell tile;
    [SerializeField] private int gridHeight = 5;
    [SerializeField] private int gridWidth = 5;
    [SerializeField] private float tileSize = 2f;
    [SerializeField] private Tilemap tileMap;

    public Cell[,] Tiles { get; private set; }
    public int Height => gridHeight;
    public int Width => gridWidth;

    private void Awake()
    {
        GenerateGrid();
    }

    private void GenerateGrid()
    {
        Tiles = new Cell[gridHeight, gridWidth];
        for(int x=0; x< gridHeight; x++)
        {
            for (int y = 0; y < gridWidth; y++)
            {
                Cell newTile = Instantiate(tile,transform);
                float posX = -22f + (x * tileSize + y * tileSize) / 2f; 
                float posY =  12.9f + (x * tileSize - y * tileSize) / 3.4f; 
                newTile.transform.position = new Vector2(posX, posY);
                Vector3Int pos = new Vector3Int(x, y, 0);
                newTile.name = x + ", " + y;
                Tiles[x,y] = newTile;
            }
        }
    }

}
