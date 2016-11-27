namespace GameDesign.Source.Mappers
{
    using System;
    using System.Collections.Generic;
    using Enumerations;

    public class ConsoleColorMapper : BaseColorMapper<ConsoleColor>
    {
        public override IDictionary<Color, ConsoleColor> GetGameColorConvertedToOuterColor =>
            this.OuterColor();

        public override IDictionary<ConsoleColor, Color> GetOuterColorConvertedToGameColor =>
            this.GameColor();

        private IDictionary<Color, ConsoleColor> OuterColor()
        {
            return new Dictionary<Color, ConsoleColor>
            {
                { Color.Black, ConsoleColor.Black },
                { Color.Blue, ConsoleColor.Blue },
                { Color.LightGray, ConsoleColor.Gray },
                { Color.Gray, ConsoleColor.DarkGray },
                { Color.Green, ConsoleColor.Green },
                { Color.Red, ConsoleColor.Red },
                { Color.White, ConsoleColor.White },
                { Color.Yellow, ConsoleColor.Yellow }
            };
        }

        private IDictionary<ConsoleColor, Color> GameColor()
        {
            return new Dictionary<ConsoleColor, Color>
            {
                { ConsoleColor.Black, Color.Black },
                { ConsoleColor.Blue, Color.Blue },
                { ConsoleColor.Gray, Color.LightGray },
                { ConsoleColor.DarkGray, Color.Gray },
                { ConsoleColor.Green, Color.Green },
                { ConsoleColor.Red, Color.Red },
                { ConsoleColor.White, Color.White },
                { ConsoleColor.Yellow, Color.Yellow }
            };
        }
    }
}