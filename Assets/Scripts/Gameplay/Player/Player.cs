using System;
using System.Collections.Generic;
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
                    ICell cell = _field.GetCell(hitPoint);
                    _clickedItem = cell.FindItem();
                    _clickedItemStartPosition = _clickedItem?.Position ?? _clickedItemStartPosition;
                }
            }

            if (Input.GetMouseButton(0) && IsMovingItem)
                _clickedItem.Teleport(_camera.ScreenToWorldPoint(Input.mousePosition));

            if (Input.GetMouseButtonUp(0) && IsMovingItem)
                Put(_clickedItem);
        }

        private void Put(IItem item)
        {
            List<ICell> neighbours = _field.FindBusyNeighbours(item);

            if (_field.CanMerge(neighbours))
            {
                _field.Merge(neighbours);
            }

            else
            {
                ICell cell = _field.GetCell(item.Position);
                
                if (!cell.IsEmpty(item))
                    item.Teleport(_clickedItemStartPosition);
            }

            _clickedItem = null;
        }
    }
}