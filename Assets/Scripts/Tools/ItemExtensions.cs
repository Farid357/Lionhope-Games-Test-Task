using LionhopeGamesTest.Gameplay;

namespace LionhopeGamesTest.Tools
{
    public static class ItemExtensions
    {
        public static bool HasSameData(this IItem item, IItem anotherItem)
        {
            int level = item.Data.Level;
            Chain chain = item.Data.Chain;
            return anotherItem.Data.Level == level && anotherItem.Data.Chain == chain;
        }
    }
}