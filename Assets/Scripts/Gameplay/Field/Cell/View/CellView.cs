using System;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace LionhopeGamesTest.Gameplay
{
    public class CellView : ICellView
    {
        private readonly Tilemap _tilemap;
        private readonly TileBase _tile;
        private readonly TileBase _highlightedTile;

        public CellView(Tilemap tilemap, Vector3Int position, TileBase tile, TileBase highlightedTile)
        {
            _tilemap = tilemap ? tilemap : throw new ArgumentNullException(nameof(tilemap));
            Position = position;
            _tile = tile ? tile : throw new ArgumentNullException(nameof(tile));
            _highlightedTile = highlightedTile ? highlightedTile : throw new ArgumentNullException(nameof(highlightedTile));
        }

        public Vector3Int Position { get; }
        
        public void Select()
        {
            _tilemap.SetTile(Position, _highlightedTile);
        }

        public void Unselect()
        {
            _tilemap.SetTile(Position, _tile);
        }
    }
}