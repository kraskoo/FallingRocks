namespace GameDesign.Source.Mappers
{
    using System;
    using System.Collections.Generic;
    using Enumerations;
    using Interfaces;

    public abstract class BaseColorMapper<T> : IColorConvertible<T>
        where T : IComparable, IFormattable, IConvertible
    {
        public abstract IDictionary<Color, T> GetGameColorConvertedToOuterColor { get; }

        public abstract IDictionary<T, Color> GetOuterColorConvertedToGameColor { get; }
    }
}