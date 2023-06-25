using System.Linq;
using UnityEngine;

namespace LionhopeGamesTest.Gameplay
{
    public class ItemsFactory : MonoBehaviour, IItemsFactory
    {
        [SerializeField] private Item _prefab;

        private ItemData[] _allItemsData;

        private void Awake()
        {
            _allItemsData = Resources.LoadAll("Data", typeof(ItemData)).Cast<ItemData>().ToArray();
        }

        public void Create(ItemData mergeItemsData, Vector2 spawnPosition)
        {
            ItemData nextData = _allItemsData.First(data => data.Chain == mergeItemsData.Chain && data.Level == mergeItemsData.Level + 1);
            Item item = Instantiate(_prefab, spawnPosition, Quaternion.identity);
            item.Init(nextData);
        }
    }
}