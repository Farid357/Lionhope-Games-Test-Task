using System;
using System.Collections.Generic;
using System.Linq;
using LionhopeGamesTest.Tools;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace LionhopeGamesTest.Gameplay
{
    public class Field : IField
    {
        private readonly ICell[,] _cells;
        private readonly IItemsFactory _itemsFactory;
        private readonly Tilemap _tilemap;

        public Field(ICell[,] cells, IItemsFactory itemsFactory, Tilemap tilemap)
        {
            _cells = cells ?? throw new ArgumentNullException(nameof(cells));
            _itemsFactory = itemsFactory ?? throw new ArgumentNullException(nameof(itemsFactory));
            _tilemap = tilemap ? tilemap : throw new ArgumentNullException(nameof(tilemap));
        }

        public List<ICell> FindSameNeighbours(IItem item) => _cells.FindSameNeighboursTo(item);

        public bool HasCell(Vector2 position) => _tilemap.HasTile(_tilemap.WorldToCell(position));

        public bool IsItemInAnyOther(IItem item) => _cells.IsInAny(item);

        public ICell GetCell(Vector2 position)
        {
            if (!HasCell(position))
                throw new InvalidOperationException(nameof(HasCell));

            Vector3Int cellPosition = _tilemap.WorldToCell(position);
            return _cells[cellPosition.x, cellPosition.y];
        }

        public bool CanMerge(List<ICell> cells)
        {
            if (cells.Count < 2)
                return false;

            ICell cell = cells.First();

            if (cell.IsEmpty)
                return false;

            IItem item = cell.FindItem();

            if (item.CanBeMerged() == false)
                return false;

            return cells.All(c => !c.IsEmpty && c.FindItem().HasSameData(item));
        }

        public void Merge(List<ICell> cells)
        {
            if (!CanMerge(cells))
                throw new InvalidOperationException(nameof(CanMerge));

            cells.ForEach(cell => cell.FindItem().Disable());
            ItemData itemData = cells.First().FindItem().Data;
            int leftItemsCount = (cells.Count + 1).LeftItemsCount();
         
            for (int i = 0; i < leftItemsCount; i++)
                _itemsFactory.Create(itemData, cells[i + 1].View.Position);

            for (int i = 0; i < (cells.Count + 1).NewItemsCount(); i++)
                _itemsFactory.CreateNextLevelItem(itemData, cells[leftItemsCount + i + 1].View.Position);
        }

        public void UnselectCells()
        {
            foreach (ICell cell in _cells)
            {
                cell.View.Unselect();
            }
        }
    }
}