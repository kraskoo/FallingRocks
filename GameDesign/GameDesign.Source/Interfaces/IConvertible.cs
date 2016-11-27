namespace GameDesign.Source.Interfaces
{
    using System;

    public interface IConvertible<T>
        where T : IComparable, IFormattable, IConvertible
    {
    }
}