using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LionhopeGamesTest.Gameplay
{
    public class ItemsFactory : MonoBehaviour, IItemsFactory
    {
        [SerializeField] private Item _prefab;

        private IReadOnlyList<ItemData> _allItemsData;

        public void Init(IReadOnlyList<ItemData> itemsData)
        {
            if (itemsData.Count == 0)
                throw new ArgumentException("Value cannot be an empty collection.", nameof(itemsData));
            
            _allItemsData = itemsData;
        }

        public IItem Create(ItemData mergeItemsData, Vector2 spawnPosition)
        {
            Item item = Instantiate(_prefab, spawnPosition, Quaternion.identity);
            item.Init(mergeItemsData);
            return item;
        }

        public IItem CreateNextLevelItem(ItemData mergeItemsData, Vector2 spawnPosition)
        {
            ItemData nextData = _allItemsData.First(data => data.Chain == mergeItemsData.Chain && data.Level == mergeItemsData.Level + 1);
            return Create(nextData, spawnPosition);
        }
    }
}