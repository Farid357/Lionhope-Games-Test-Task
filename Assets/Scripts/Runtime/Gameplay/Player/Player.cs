using System;
using UnityEngine;
using LionhopeGamesTest.Tools;

namespace LionhopeGamesTest.Gameplay
{
    public class Player : MonoBehaviour
    {
        private IField _field;
        private IPlayerInput _input;
        private Vector3 _clickedItemStartPosition;

        public static IItem ClickedItem { get; private set; }

        public void Init(IField field, IPlayerInput input)
        {
            _field = field ?? throw new ArgumentNullException(nameof(field));
            _input = input ?? throw new ArgumentNullException(nameof(input));
        }

        private void Update()
        {
            if (_input.LeftMouseButtonDown)
                ClickedItem = TryTakeItem();

            if (ClickedItem == null)
                return;

            if (_input.LeftMouseButtonHeldOn)
                MoveItem(ClickedItem);

            if (_input.LeftMouseButtonUp && _field.IsItemInAnyOther(ClickedItem))
                PutItem();
        }

        private IItem TryTakeItem()
        {
            Vector2 mousePosition = _input.MouseWorldPosition;
            IItem item = null;

            if (_field.HasCell(mousePosition))
            {
                item = _field.GetCell(mousePosition).FindItem();
                _clickedItemStartPosition = ClickedItem?.Position ?? _clickedItemStartPosition;
            }

            return item;
        }

        private void MoveItem(IItem item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            ClickedItem.Teleport(_input.MouseWorldPosition);

            if (_field.IsItemInAnyOther(item))
            {
                _field.SelectItemNeighbours(item);
            }

            else
            {
                _field.UnselectCells();
            }
        }

        private void PutItem()
        {
            _field.Put(ClickedItem, _clickedItemStartPosition);
            ClickedItem = null;
        }
    }
}