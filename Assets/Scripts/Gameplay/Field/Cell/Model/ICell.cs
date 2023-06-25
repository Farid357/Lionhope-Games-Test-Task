namespace LionhopeGamesTest.Gameplay
{
    public interface ICell 
    {
        bool IsEmpty { get; }

        IItem Item { get; }

        ICellView View { get; }
    }
}