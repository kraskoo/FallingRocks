namespace GameDesign.Source.Providers
{
    using System;
    using Enumerations;

    public sealed class ColorProvider
    {
        public const Color DefaultColor = Color.None;

        public static readonly Color[] Colors = InstanceColorArray();

        private ColorProvider()
        {
        }

        private static Color[] InstanceColorArray()
        {
            var colorValues = Enum.GetValues(typeof(Color));
            var colorValuesLength = colorValues.Length;
            var colorArray = new Color[colorValuesLength - 1];
            var colorsEnumerator = colorValues.GetEnumerator();
            colorsEnumerator.MoveNext();
            int index = -1;
            while (colorsEnumerator.MoveNext())
            {
                colorArray[++index] = (Color)colorsEnumerator.Current;
            }

            return colorArray;
        }
    }
}