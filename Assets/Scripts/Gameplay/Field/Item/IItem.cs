namespace LionhopeGamesTest.Gameplay
{
    public interface IItem
    {
        ItemData Data { get; }

        void Disable();
    }
}