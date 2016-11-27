namespace GameDesign.Source.Interfaces
{
    using System;
    using System.Collections.Generic;
    using Enumerations;

    public interface IKeyConvertible<T> : IConvertible<T>
        where T : IComparable, IFormattable, IConvertible
    {
        IDictionary<Key, T> GetGameKeyToOuterKey { get; }

        IDictionary<T, Key> GetOuterKeyToGameKey { get; }
    }
}