using System;
using UnityEngine;

namespace LionhopeGamesTest.Gameplay
{
    public class Cell : ICell
    {
        private readonly Collider2D[] _results;

        public Cell(ICellView view)
        {
            View = view ?? throw new ArgumentNullException(nameof(view));
            _results = new Collider2D[40];
        }

        public bool IsEmpty => FindItem() == null;

        public ICellView View { get; }

        public IItem FindItem()
        {
            Vector2 cellWorldPosition = View.Position;
            int size = Physics2D.OverlapCircleNonAlloc(cellWorldPosition, 1.2f, _results);

            for (int i = 0; i < size; i++)
            {
                if (_results[i].TryGetComponent(out IItem item))
                    return item;
            }

            return null;
        }
    }
}