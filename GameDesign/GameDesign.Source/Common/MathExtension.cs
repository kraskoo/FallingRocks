using System.Globalization;

namespace GameDesign.Source.Common
{
    public static class MathExtension
    {
        public static int HalfedValue(this int number, bool isLowerBoundRounded = true)
        {
            if (!isLowerBoundRounded)
            {
                return number / 2;
            }

            return number.IsOdd() ? (number / 2) - 1 : number / 2;
        }

        public static bool IsInBounds(this int number, int lowerBound, int upperBound)
        {
            return lowerBound >= number && number <= upperBound;
        }

        public static bool IsOdd(this int number)
        {
            return number % 2 != 0;
        }
    }
}