using System;

namespace LionhopeGamesTest.Gameplay
{
    public class Cell : ICell
    {
        private IItem _item;

        public Cell(ICellView view)
        {
            View = view ?? throw new ArgumentNullException(nameof(view));
        }

        public bool IsEmpty => _item == null;

        public IItem Item => IsEmpty ? throw new InvalidOperationException(nameof(IsEmpty)) : _item;

        public ICellView View { get; }

        public void Clear()
        {
            _item = null;
        }

        public void PutItem(IItem item)
        {
            _item = item ?? throw new ArgumentNullException(nameof(item));
        }
    }
}