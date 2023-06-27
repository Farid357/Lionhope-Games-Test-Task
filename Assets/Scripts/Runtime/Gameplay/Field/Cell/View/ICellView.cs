using UnityEngine;

namespace LionhopeGamesTest.Gameplay
{
    public interface ICellView
    {
        Vector2 Position { get; }

        void Select();

        void Unselect();
    }
}