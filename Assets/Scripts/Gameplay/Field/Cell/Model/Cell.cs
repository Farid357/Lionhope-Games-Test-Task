using System;
using UnityEngine;

namespace LionhopeGamesTest.Gameplay
{
    public class Cell : ICell
    {
        public Cell(ICellView view)
        {
            View = view ?? throw new ArgumentNullException(nameof(view));
        }

        public bool IsEmpty => Item == null;

        public ICellView View { get; }

        public IItem Item
        {
            get
            {
                Vector2 cellWorldPosition = View.Position;
                Collider2D hit = Physics2D.OverlapCircle(cellWorldPosition,  1.2f);

                if (hit != null && hit.TryGetComponent(out IItem item))
                    return item;

                return null;
            }
        }
    }
}