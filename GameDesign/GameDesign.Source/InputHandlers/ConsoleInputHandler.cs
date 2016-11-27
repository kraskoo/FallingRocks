namespace GameDesign.Source.InputHandlers
{
    using System;
    using Enumerations;
    using Interfaces;
    using Mappers;

    public class ConsoleInputHandler : BaseInputHandler<ConsoleKey>
    {
        private readonly IKeyConvertible<ConsoleKey> mapper;

        public ConsoleInputHandler()
        {
            this.mapper = new ConsoleKeyMapper();
        }

        public override bool HasPressedKey => Console.KeyAvailable;

        public override Key Key
        {
            get
            {
                var key = this.ReadKey();
                if (!this.mapper.GetOuterKeyToGameKey.ContainsKey(key))
                {
                    return Key.NotNeededKey;
                }

                return this.mapper.GetOuterKeyToGameKey[key];
            }
        }

        private ConsoleKey ReadKey()
        {
            return Console.ReadKey().Key;
        }
    }
}