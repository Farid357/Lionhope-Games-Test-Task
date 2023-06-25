using System.Collections.Generic;
using UnityEngine;

namespace LionhopeGamesTest.Gameplay
{
    public interface IField
    {
        List<ICell> FindBusyNeighbours(IItem item);

        bool IsItemInAnyOther(IItem item);
        
        bool CanMerge(List<ICell> cells);
        
        bool HasCell(Vector2 position);
        
        ICell GetCell(Vector2 position);
        
        void Merge(List<ICell> cells);

        void UnselectCells();
    }
}