namespace GameDesign.Source.Interfaces
{
    using System;
    using System.Collections.Generic;
    using Enumerations;

    public interface IColorConvertible<T> : IConvertible<T>
        where T : IComparable, IFormattable, IConvertible
    {
        IDictionary<Color, T> GetGameColorConvertedToOuterColor { get; }

        IDictionary<T, Color> GetOuterColorConvertedToGameColor { get; }
    }
}