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

        public List<ICell> FindBusyNeighbours(IItem item) => _cells.GetBusyNeighboursTo(item);

        public bool HasCell(Vector2 position)
        {
            return _cells.Cast<ICell>().Any(cell => Vector2.Distance(cell.View.Position, position) <= 15f);
        }

        public ICell GetCell(Vector2 position)
        {
            if (!HasCell(position))
                throw new InvalidOperationException(nameof(HasCell));

            return _cells.Cast<ICell>().OrderBy(cell => Vector2.Distance(cell.View.Position, position)).First(cell => Vector2.Distance(cell.View.Position, position) <= 15f);
        }

        public bool CanMerge(List<ICell> cells)
        {
            return false;
            ICell cell = cells.First();
            Chain chain = cell.Item.Data.Chain;
            
            if (cell.IsEmpty)
                return false;
            
            int level = cell.Item.Data.Level;

            if (level == 3)
                return false;
            
            return cells.All(cell1 => !cell1.IsEmpty && cell1.Item.Data.Level == level && cell1.Item.Data.Chain == chain);
        }

        public void Merge(List<ICell> cells)
        {
            if (!CanMerge(cells))
                throw new InvalidOperationException(nameof(CanMerge));

            cells.ForEach(cell => cell.Item.Disable());
        }
    }
}