namespace LionhopeGamesTest.Gameplay
{
    public interface ICell
    {
        bool IsEmpty { get; }

        IItem FindItem();
        IItem FindItemExcept(IItem item);
        ICellView View { get; }
    }
}