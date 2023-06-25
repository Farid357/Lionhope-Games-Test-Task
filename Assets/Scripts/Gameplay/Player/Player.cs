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
                    
                    if (cell.IsEmpty)
                    {
                        _clickedItem = null;
                        return;
                    }

                    _clickedItem = cell.Item;
                    _clickedItemStartPosition = _clickedItem.Position;
                }
            }

            if (Input.GetMouseButton(0) && IsMovingItem)
                _clickedItem.Teleport(_camera.ScreenToWorldPoint(Input.mousePosition));

            if (Input.GetMouseButtonUp(0) && IsMovingItem)
                PutItem();
        }

        private void PutItem()
        {
            List<ICell> neighbours = _field.FindBusyNeighbours(_clickedItem);

            if (_field.CanMerge(neighbours))
            {
                _field.Merge(neighbours);
            }

            else
            {
                _clickedItem.Teleport(_clickedItemStartPosition);
            }

            _clickedItem = null;
        }
    }
}