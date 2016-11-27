namespace GameDesign.Source.Mappers
{
    using System;
    using System.Collections.Generic;
    using Enumerations;

    public class ConsoleKeyMapper : BaseKeyMapper<ConsoleKey>
    {
        public override IDictionary<Key, ConsoleKey> GetGameKeyToOuterKey =>
            this.OuterKey();

        public override IDictionary<ConsoleKey, Key> GetOuterKeyToGameKey =>
            this.GameKey();

        private IDictionary<Key, ConsoleKey> OuterKey()
        {
            return new Dictionary<Key, ConsoleKey>
            {
                { Key.Up, ConsoleKey.UpArrow },
                { Key.Down, ConsoleKey.DownArrow },
                { Key.Left, ConsoleKey.LeftArrow },
                { Key.Right, ConsoleKey.RightArrow },
                { Key.Enter, ConsoleKey.Enter },
                { Key.Escape, ConsoleKey.Escape },
                { Key.NotNeededKey, ConsoleKey.Home }
            };
        }

        private IDictionary<ConsoleKey, Key> GameKey()
        {
            return new Dictionary<ConsoleKey, Key>
            {
                { ConsoleKey.UpArrow, Key.Up },
                { ConsoleKey.DownArrow, Key.Down },
                { ConsoleKey.LeftArrow, Key.Left },
                { ConsoleKey.RightArrow, Key.Right },
                { ConsoleKey.Enter, Key.Enter },
                { ConsoleKey.Escape, Key.Escape },
                { ConsoleKey.Home, Key.NotNeededKey }
            };
        }
    }
}