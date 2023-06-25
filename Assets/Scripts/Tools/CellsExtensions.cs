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

        private static readonly Vector2Int[] _directions = new Vector2Int[]
        {
            Vector2Int.down,
            Vector2Int.up,
            Vector2Int.left,
            Vector2Int.right,
        };

        public static Vector2Int GetPositionTo(this ICell[,] cells, IItem item)
        {
            for (int x = 0; x < cells.GetLength(0); x++)
            {
                for (int y = 0; y < cells.GetLength(1); y++)
                {
                    if (!cells[x, y].IsEmpty && cells[x, y].FindItem().Equals(item))
                        return new Vector2Int(x, y);
                }
            }

            throw new InvalidOperationException($"Hasn't found position to {item}");
        }

        public static List<ICell> GetBusyNeighboursTo(this ICell[,] cells, IItem item)
        {
            _neighbours.Clear();
            Vector2Int placedOnCellPosition = GetPositionTo(cells, item);
            _neighbours.Add(cells[placedOnCellPosition.x, placedOnCellPosition.y]);
            return GetBusyNeighboursToLoop(cells, item);
        }

        private static List<ICell> GetBusyNeighboursToLoop(this ICell[,] cells, IItem item)
        {
            Vector2Int position = cells.GetPositionTo(item);

            foreach (var direction in _directions)
            {
                var neighbourPosition = position + direction;
                if (InBounds(cells, neighbourPosition))
                {
                    var neighbour = cells[neighbourPosition.x, neighbourPosition.y];

                    if (!neighbour.IsEmpty && !_neighbours.Contains(neighbour))
                    {
                        _neighbours.Add(neighbour);
                        List<ICell> neighboursToNeighbour = cells.GetBusyNeighboursToLoopWithoutRepeat(neighbour.FindItem());

                        foreach (ICell cell in neighboursToNeighbour)
                        {
                            if (!_neighbours.Contains(cell))
                                _neighbours.Add(cell);
                        }
                    }
                }
            }
            
            return _neighbours;
        }

        private static List<ICell> GetBusyNeighboursToLoopWithoutRepeat(this ICell[,] cells, IItem item)
        {
            Vector2Int position = cells.GetPositionTo(item);

            return _directions.Select(direction => position + direction)
                .Where(neighbourPosition => InBounds(cells, neighbourPosition))
                .Select(neighbourPosition => cells[neighbourPosition.x, neighbourPosition.y])
                .Where(neighbour => !neighbour.IsEmpty && !_neighbours.Contains(neighbour)).ToList();
        }

        private static ICell CellWith(ICell[,] cells, IItem item)
        {
            foreach (ICell cell in cells)
            {
                if (!cell.IsEmpty && cell.FindItem() == item)
                    return cell;
            }

            throw new InvalidOperationException();
        }

        public static bool IsInAny(this ICell[,] cells, IItem item)
        {
            List<ICell> cellsList = cells.Cast<ICell>().ToList();
            return cellsList.Any(cell => !cell.IsEmpty && cell.FindItem() != item && cell.FindItem().Position == item.Position);
        }

        private static bool InBounds(ICell[,] cells, Vector2Int position)
        {
            return position.x < cells.GetLength(0) && position.y < cells.GetLength(1) && position.x >= 0 && position.y >= 0;
        }
    }
}