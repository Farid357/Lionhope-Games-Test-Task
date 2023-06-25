using UnityEngine;

namespace LionhopeGamesTest.Gameplay
{
    public interface IItemsFactory
    {
        void Create(ItemData mergeItemsData, Vector2 spawnPosition);
    }
}