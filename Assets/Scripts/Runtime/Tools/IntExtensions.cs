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
        
        public static int NewItemsCount(this int number)
        {
            double n = (-1f - number) / 6f * -1;

            if (n % 1 == 0)
                return (int)(n * 2);

            int remainder = number % 3;

            if (remainder > 0)
                number -= remainder;
            
            return number / 3;
        }
    }
}