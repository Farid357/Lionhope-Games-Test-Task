using System;
using UnityEngine;

namespace LionhopeGamesTest.Gameplay
{
    public class Cell : ICell
    {
        private readonly float _findItemRadius;
        private readonly Collider2D[] _results;

        public Cell(ICellView view, float findItemRadius = 0.4f)
        {
            View = view ?? throw new ArgumentNullException(nameof(view));
            _findItemRadius = findItemRadius;
            _results = new Collider2D[10];
        }

        public bool IsEmpty => FindItem() == null;

        public ICellView View { get; }

        public IItem FindItem()
        {
            Vector2 cellWorldPosition = View.Position;
            int size = Physics2D.OverlapCircleNonAlloc(cellWorldPosition, _findItemRadius, _results);

            for (int i = 0; i < size; i++)
            {
                if (_results[i].TryGetComponent(out IItem item) && item != Player.ClickedItem)
                    return item;
            }

            return null;
        }
    }
}