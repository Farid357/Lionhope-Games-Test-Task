namespace LionhopeGamesTest.Gameplay
{
    public interface ICell
    {
        bool IsEmpty { get; }

        IItem FindItem();
        
        ICellView View { get; }
    }
}