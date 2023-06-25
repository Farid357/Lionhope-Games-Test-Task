using System;
using System.Collections.Generic;
using System.Linq;
using LionhopeGamesTest.Gameplay;
using UnityEngine;

namespace LionhopeGamesTest.Tools
{
    public static class Extensions
    {
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
                    if (cells[x, y].View.Position.Equals(item.Position))
                        return new Vector2Int(x, y);
                }
            }

            throw new InvalidOperationException($"Hasn't find {item}");
        }

        public static List<ICell> GetBusyNeighboursTo(this ICell[,] cells, IItem item)
        {
           Vector2Int position = cells.GetPositionTo(item);
           List<ICell> neighbours = new List<ICell>();
           
           foreach (var direction in _directions)
           {
               var neighbourPosition = position + direction;
               if (InBounds(cells, neighbourPosition))
               {
                   var neighbour = cells[neighbourPosition.x, neighbourPosition.y];
           
                   if (!neighbour.IsEmpty())
                   {
                       neighbours.Add(neighbour);
                    //   neighbours.AddRange(cells.GetBusyNeighboursTo(neighbour));
                   }
               }
           }

           return new List<ICell>();
           return neighbours.Distinct().ToList();
        }

        private static bool InBounds(ICell[,] cells, Vector2Int position)
        {
            return position.x < cells.GetLength(0) && position.y < cells.GetLength(1) && position.x >= 0 &&
                   position.y >= 0;
        }
    }
}