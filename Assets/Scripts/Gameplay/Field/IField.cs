using System.Collections.Generic;

namespace LionhopeGamesTest.Gameplay
{
    public interface IField
    {
        List<ICell> FindBusyNeighbours(ICell cell);
    }
}