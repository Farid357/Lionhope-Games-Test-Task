using System.Collections.Generic;
using LionhopeGamesTest.Gameplay;
using UnityEngine;

namespace LionhopeGamesTest.Tools
{
    public static class FieldExtensions
    {
        public static void Put(this IField field, IItem item, Vector2 startItemPosition)
        {
            List<ICell> neighbours = field.FindSameNeighbours(item);
            field.UnselectCells();

            if (field.CanMerge(neighbours))
            {
                field.Merge(neighbours);
                item.Disable();
            }

            else if (field.IsItemInAnyOther(item))
            {
                item.Teleport(startItemPosition);
            }
        }

        public static void SelectItemNeighbours(this IField field, IItem item)
        {
            List<ICell> neighbours = field.FindSameNeighbours(item);

            if (field.CanMerge(neighbours))
                neighbours.ForEach(cell => cell.View.Select());
        }
    }
}