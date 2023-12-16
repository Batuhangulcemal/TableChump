namespace AsepStudios.Mechanic.GameCore
{
    public static class CardPointCalculator
    {
        public static int Calculate(int cardNumber)
        {
            if (cardNumber.IsDivisible(100))
            {
                return 25;
            }
            if (cardNumber.IsDivisible(50))
            {
                return 10;
            }
            if (cardNumber.IsDivisible(10))
            {  
                return 5;
            }
            if (cardNumber.IsDivisible(5))
            {
                return 2;
            }
            return 1;
        }


        private static bool IsDivisible(this int number, int divider)
        {
            return number % divider == 0;
        }
    }
}