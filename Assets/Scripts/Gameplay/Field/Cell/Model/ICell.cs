namespace LionhopeGamesTest.Gameplay
{
    public interface ICell
    {
        bool IsEmpty(IItem except = null);

        IItem FindItem(IItem exceptItem = null);

        ICellView View { get; }
    }
}