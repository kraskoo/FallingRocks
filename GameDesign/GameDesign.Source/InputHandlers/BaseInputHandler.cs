namespace GameDesign.Source.InputHandlers
{
    using System;
    using Enumerations;
    using Interfaces;

    public abstract class BaseInputHandler<T> : IKeyProvider, IConvertible<T>
        where T : IComparable, IFormattable, IConvertible
    {
        public abstract bool HasPressedKey { get; }

        public abstract Key Key { get; }
    }
}