namespace LionhopeGamesTest.Gameplay
{
    public interface IReadOnlyCell
    {
        bool IsEmpty { get; }

        IItem Item { get; }

        ICellView View { get; }
    }
}