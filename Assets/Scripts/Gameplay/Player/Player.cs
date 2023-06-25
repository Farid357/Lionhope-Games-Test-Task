using System;
using System.Collections.Generic;
using System.Linq;
using LionhopeGamesTest.Tools;
using UnityEngine;

namespace LionhopeGamesTest.Gameplay
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private Camera _camera;

        private IField _field;
        private IItem _clickedItem;
        private Vector3 _clickedItemStartPosition;

        public void Init(IField field)
        {
            _field = field ?? throw new ArgumentNullException(nameof(field));
        }

        private bool IsMovingItem => _clickedItem != null;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 hitPoint = _camera.ScreenToWorldPoint(Input.mousePosition);

                if (_field.HasCell(hitPoint))
                {
                    _clickedItem = _field.GetCell(hitPoint).FindItem();
                    _clickedItemStartPosition = _clickedItem?.Position ?? _clickedItemStartPosition;
                }
            }

            if (Input.GetMouseButton(0) && IsMovingItem)
            {
                _clickedItem.Teleport(_camera.ScreenToWorldPoint(Input.mousePosition));
                List<ICell> neighbours = _field.FindBusyNeighbours(_clickedItem);
                neighbours.RemoveAt(0);

                if (_field.CanMerge(neighbours))
                {
                    neighbours.ForEach(cell => cell.View.Select());
                }

                else
                {
                    _field.UnselectCells();
                }
            }

            if (Input.GetMouseButtonUp(0) && IsMovingItem)
                Put(_clickedItem);
        }

        private void Put(IItem item)
        {
            _field.Put(item, _clickedItemStartPosition);
            _clickedItem = null;
        }
    }
}