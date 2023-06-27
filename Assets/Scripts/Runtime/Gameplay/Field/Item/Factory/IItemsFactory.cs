using UnityEngine;

namespace LionhopeGamesTest.Gameplay
{
    public interface IItemsFactory
    {
        IItem Create(ItemData mergeItemsData, Vector2 spawnPosition);
        
        IItem CreateNextLevelItem(ItemData mergeItemsData, Vector2 spawnPosition);

    }
}