using System;
using System.Collections.Generic;
using System.Linq;
using LionhopeGamesTest.Tools;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace LionhopeGamesTest.Gameplay
{
    public class Field
    {
        private readonly ICell[,] _cells;
        private readonly IItemsFactory _itemsFactory;
        private readonly Tilemap _tilemap;
        private readonly List<ICell> _neighbours = new();

        private readonly Vector2Int[] _directions =
        {
            Vector2Int.down,
            Vector2Int.up,
            Vector2Int.left,
            Vector2Int.right,
        };

        public Field(ICell[,] cells, IItemsFactory itemsFactory, Tilemap tilemap)
        {
            _cells = cells ?? throw new ArgumentNullException(nameof(cells));
            _itemsFactory = itemsFactory ?? throw new ArgumentNullException(nameof(itemsFactory));
            _tilemap = tilemap ? tilemap : throw new ArgumentNullException(nameof(tilemap));
        }

        public bool HasCell(Vector2 position) => _tilemap.HasTile(_tilemap.WorldToCell(position));

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

        public void Put(IItem item, Vector2 startItemPosition)
        {
            UnselectCells();

            Debug.Log(IsItemInAnyOther(item));
            if (IsItemInAnyOther(item))
            {
                List<ICell> neighbours = FindSameNeighboursTo(item);

                if (CanMerge(neighbours))
                {
                    Merge(neighbours);
                    item.Disable();
                }
                else
                {
                    item.Teleport(startItemPosition);
                }
            }
        }

        public void SelectItemNeighbours(IItem item)
        {
            List<ICell> neighbours = FindSameNeighboursTo(item);

            if (CanMerge(neighbours))
                neighbours.ForEach(cell => cell.View.Select());
        }

        public void SelectItem(IItem item)
        {
            ICell cell = _cells.Cast<ICell>().First(cell => cell.View.Position == item.Position);
            cell.View.Select();
        }

        private ICell GetCellWithSameItemPosition(IItem item, out Vector2Int positionInArray)
        {
            for (int x = 0; x < _cells.GetLength(0); x++)
            {
                for (int y = 0; y < _cells.GetLength(1); y++)
                {
                    if (_cells[x, y].FindItem() != null && _cells[x, y].FindItem().Position == item.Position)
                    {
                        positionInArray = new Vector2Int(x, y);
                        return _cells[x, y];
                    }
                }
            }

            throw new InvalidOperationException($"Hasn't found position to {item}");
        }

        public List<ICell> FindSameNeighboursTo(IItem item)
        {
            _neighbours.Clear();
            _neighbours.Add(GetCellWithSameItemPosition(item, out _));
            return GetBusyNeighboursLoop(item).Where(cell => cell.FindItem().CanBeMerged() && cell.FindItem().HasSameData(item)).ToList();
        }

        private List<ICell> GetBusyNeighboursLoop(IItem item)
        {
            foreach (var neighbour in GetBusyNeighboursWithoutRepeat(item))
            {
                _neighbours.Add(neighbour);
                List<ICell> neighboursToNeighbour = GetBusyNeighboursWithoutRepeat(neighbour.FindItem());

                foreach (ICell cell in neighboursToNeighbour)
                {
                    _neighbours.Add(cell);
                }
            }

            return _neighbours;
        }

        private List<ICell> GetBusyNeighboursWithoutRepeat(IItem item)
        {
            GetCellWithSameItemPosition(item, out Vector2Int position);

            return (_directions.Select(direction => position + direction)
                .Where(InBounds)
                .Select(neighbourPosition => _cells[neighbourPosition.x, neighbourPosition.y])
                .Where(neighbour => !neighbour.IsEmpty && !_neighbours.Contains(neighbour))).ToList();
        }

        public bool IsItemInAnyOther(IItem item)
        {
            return _cells.Cast<ICell>().Select(cell => cell.FindItem()).Any(findItem => findItem != null && findItem.Position == item.Position);
        }

        private bool InBounds(Vector2Int position)
        {
            return position.x < _cells.GetLength(0) && position.y < _cells.GetLength(1) && position.x >= 0 && position.y >= 0;
        }
    }
}