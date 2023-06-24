namespace LionhopeGamesTest.Gameplay
{
    public interface ICell : IReadOnlyCell
    {
        void PutItem(IItem item);

        void Clear();
    }
}