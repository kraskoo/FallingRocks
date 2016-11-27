namespace GameDesign.Source.Providers
{
    using System;

    public sealed class RandomProvider
    {
        private static readonly Random Random = new Random();

        public static int Next(int minValue, int maxValue)
        {
            return Random.Next(minValue, maxValue);
        }
    }
}