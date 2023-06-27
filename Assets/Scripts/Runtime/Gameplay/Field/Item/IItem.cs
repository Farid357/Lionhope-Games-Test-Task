using UnityEngine;

namespace LionhopeGamesTest.Gameplay
{
    public interface IItem
    {
        ItemData Data { get; }
        
        Vector2 Position { get; }

        void Teleport(Vector2 position);
        
        void Disable();
    }
}