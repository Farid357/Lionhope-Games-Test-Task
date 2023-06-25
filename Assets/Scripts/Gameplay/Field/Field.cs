using System;
using System.Collections.Generic;
using System.Linq;
using LionhopeGamesTest.Tools;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace LionhopeGamesTest.Gameplay
{
    public class Field : IField
    {
        private readonly ICell[,] _cells;
        private readonly Tilemap _tilemap;

        public Field(ICell[,] cells, Tilemap tilemap)
        {
            _cells = cells ?? throw new ArgumentNullException(nameof(cells));
            _tilemap = tilemap ? tilemap : throw new ArgumentNullException(nameof(tilemap));
        }

        public List<ICell> FindBusyNeighbours(IItem item) => _cells.GetBusyNeighboursTo(item);

        public bool HasCell(Vector2 position)
        {
            return _tilemap.HasTile(_tilemap.WorldToCell(position));
        }

        public ICell GetCell(Vector2 position)
        {
            if (!HasCell(position))
                throw new InvalidOperationException(nameof(HasCell));

            Vector3Int cellPosition = _tilemap.WorldToCell(position);
            return _cells[cellPosition.x, cellPosition.y];
        }

        public bool CanMerge(List<ICell> cells)
        {
            return false;
            ICell cell = cells.First();
            Chain chain = cell.FindItem().Data.Chain;

            if (cell.IsEmpty())
                return false;

            int level = cell.FindItem().Data.Level;

            if (level == 3)
                return false;

            return cells.All(cell1 => !cell1.IsEmpty() && cell1.FindItem().Data.Level == level && cell1.FindItem().Data.Chain == chain);
        }

        public void Merge(List<ICell> cells)
        {
            if (!CanMerge(cells))
                throw new InvalidOperationException(nameof(CanMerge));

            cells.ForEach(cell => cell.FindItem().Disable());
        }
    }
}