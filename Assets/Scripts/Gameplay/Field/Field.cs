using System;
using System.Collections.Generic;
using System.Linq;
using LionhopeGamesTest.Tools;
using UnityEngine;

namespace LionhopeGamesTest.Gameplay
{
    public class Field : IField
    {
        private readonly ICell[,] _cells;

        public Field(ICell[,] cells)
        {
            _cells = cells ?? throw new ArgumentNullException(nameof(cells));
        }

        public List<ICell> FindBusyNeighbours(ICell cell) => _cells.GetBusyNeighboursTo(cell);

        public bool CanMerge(List<ICell> cells)
        {
            ICell cell = cells.First();
        
            if (cell.IsEmpty)
                return false;
            
            int level = cell.Item.Data.Level;
            return cells.All(cell1 => !cell1.IsEmpty && cell1.Item.Data.Level == level);
        }

        public bool HasCell(Vector3Int position)
        {
            //TODO
            return false;
        }
        
        public void Merge(List<ICell> cells)
        {
            if (!CanMerge(cells))
                throw new InvalidOperationException(nameof(CanMerge));

            cells.ForEach(cell => cell.Item.Disable());
        }
    }
}