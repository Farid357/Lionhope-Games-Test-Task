using System;
using UnityEngine;
using LionhopeGamesTest.Tools;

namespace LionhopeGamesTest.Gameplay
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private Camera _camera;

        private IField _field;
        private Vector3 _clickedItemStartPosition;

        public static IItem ClickedItem { get; private set; }

        public void Init(IField field)
        {
            _field = field ?? throw new ArgumentNullException(nameof(field));
        }

        private bool IsMovingItem => ClickedItem != null;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 hitPoint = _camera.ScreenToWorldPoint(Input.mousePosition);

                if (_field.HasCell(hitPoint))
                {
                    ClickedItem = _field.GetCell(hitPoint).FindItem();
                    _clickedItemStartPosition = ClickedItem?.Position ?? _clickedItemStartPosition;
                }
            }

            if (Input.GetMouseButton(0) && IsMovingItem)
            {
                ClickedItem.Teleport(_camera.ScreenToWorldPoint(Input.mousePosition));

                if (_field.IsItemInAnyOther(ClickedItem))
                {
                    _field.SelectItemNeighbours(ClickedItem);
                }

                else
                {
                    _field.UnselectCells();
                }
            }

            if (Input.GetMouseButtonUp(0) && IsMovingItem && _field.IsItemInAnyOther(ClickedItem))
            {
                _field.Put(ClickedItem, _clickedItemStartPosition);
                ClickedItem = null;
            }
        }
    }
}