using UnityEngine;
using UnityEngine.Tilemaps;

namespace LionhopeGamesTest.Gameplay
{
    public class FieldFactory : MonoBehaviour
    {
        [SerializeField] private Tilemap _tileMap;
        [SerializeField] private Tile _tile;
        [SerializeField] private Tile _highlightedTile;

        public IField Create()
        {
            const int width = 10;
            var cells = new ICell[width, width];
            
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < width; y++)
                {
                    var position = new Vector3Int(x, y, 0);
                    _tileMap.SetTile(position, _tile);
                    cells[x, y] = new Cell(new CellView(_tileMap, position, _tileMap.GetTile(position), _highlightedTile));
                }
            }
            return new Field(cells);
        }
    }
}