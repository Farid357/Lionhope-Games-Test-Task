using System;
using System.Collections.Generic;
using System.Linq;
using LionhopeGamesTest.Gameplay;
using UnityEngine;

namespace LionhopeGamesTest.Tools
{
    public static class CellsExtensions
    {
        private static readonly List<ICell> _neighbours = new();

        private static readonly Vector2Int[] _directions =
        {
            Vector2Int.down,
            Vector2Int.up,
            Vector2Int.left,
            Vector2Int.right,
        };

        private static ICell GetCellWithSameItemPosition(this ICell[,] cells, IItem item, out Vector2Int positionInArray)
        {
            for (int x = 0; x < cells.GetLength(0); x++)
            {
                for (int y = 0; y < cells.GetLength(1); y++)
                {
                    if (cells[x, y].FindItem() != null && cells[x, y].FindItem().Position == item.Position)
                    {
                        positionInArray = new Vector2Int(x, y);
                        return cells[x, y];
                    }
                }
            }

            throw new InvalidOperationException($"Hasn't found position to {item}");
        }

        public static List<ICell> FindSameNeighboursTo(this ICell[,] cells, IItem item)
        {
            _neighbours.Clear();
            _neighbours.Add(GetCellWithSameItemPosition(cells, item, out _));
            return GetBusyNeighboursToLoop(cells, item).Where(cell => cell.FindItem().Data.Level < 3 && cell.FindItem().HasSameData(item)).ToList();
        }

        private static List<ICell> GetBusyNeighboursToLoop(this ICell[,] cells, IItem item)
        {
            foreach (var neighbour in cells.GetBusyNeighboursToLoopWithoutRepeat(item))
            {
                _neighbours.Add(neighbour);
                List<ICell> neighboursToNeighbour = cells.GetBusyNeighboursToLoopWithoutRepeat(neighbour.FindItem());

                foreach (ICell cell in neighboursToNeighbour)
                {
                    _neighbours.Add(cell);
                }
            }

            return _neighbours;
        }

        private static List<ICell> GetBusyNeighboursToLoopWithoutRepeat(this ICell[,] cells, IItem item)
        {
            GetCellWithSameItemPosition(cells, item, out Vector2Int position);
            List<ICell> neighbours = new List<ICell>();

            foreach (var direction in _directions)
            {
                var neighbourPosition = position + direction;
                if (InBounds(cells, neighbourPosition))
                {
                    var neighbour = cells[neighbourPosition.x, neighbourPosition.y];
                    if (!neighbour.IsEmpty && !_neighbours.Contains(neighbour))
                        neighbours.Add(neighbour);
                }
            }

            return neighbours;
        }

        public static bool IsInAny(this ICell[,] cells, IItem item)
        {
            foreach (ICell cell in cells)
            {
                if (cell.FindItem() != null && cell.FindItem().Position == item.Position)
                    return true;
            }

            return false;
        }

        private static bool InBounds(ICell[,] cells, Vector2Int position)
        {
            return position.x < cells.GetLength(0) && position.y < cells.GetLength(1) && position.x >= 0 && position.y >= 0;
        }
    }
}