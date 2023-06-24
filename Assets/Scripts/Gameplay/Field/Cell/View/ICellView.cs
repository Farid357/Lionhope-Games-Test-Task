using UnityEngine;

namespace LionhopeGamesTest.Gameplay
{
    public interface ICellView
    {
        Vector3Int Position { get; }
        
        void Select();

        void Unselect();
    }
}