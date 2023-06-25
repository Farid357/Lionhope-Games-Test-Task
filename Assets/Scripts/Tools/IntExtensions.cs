namespace LionhopeGamesTest.Tools
{
    public static class IntExtensions
    {
        public static int LeftItemsCount(this int number)
        {
            double n = (-1f - number) / 6f * -1;

            if (n % 1 == 0)
                return 0;

            return number % 3;
        }
    }
}