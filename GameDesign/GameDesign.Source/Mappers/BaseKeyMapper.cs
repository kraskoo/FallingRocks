namespace GameDesign.Source.Mappers
{
    using System;
    using System.Collections.Generic;
    using Enumerations;
    using Interfaces;

    public abstract class BaseKeyMapper<T> : IKeyConvertible<T>
        where T : IComparable, IFormattable, IConvertible
    {
        public abstract IDictionary<Key, T> GetGameKeyToOuterKey { get; }

        public abstract IDictionary<T, Key> GetOuterKeyToGameKey { get; }
    }
}