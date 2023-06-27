using UnityEngine;

namespace LionhopeGamesTest.Gameplay
{
    public interface IPlayerInput
    {
        Vector2 MouseWorldPosition { get; }
        
        bool LeftMouseButtonDown { get; }
        
        bool LeftMouseButtonHeldOn { get; }
        
        bool LeftMouseButtonUp { get; }

    }
}