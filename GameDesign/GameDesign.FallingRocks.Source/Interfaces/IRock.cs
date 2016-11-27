namespace GameDesign.FallingRocks.Source.Interfaces
{
    using System;
    using GameDesign.Source.Enumerations;

    public interface IRock : IEntity, IComparable<IRock>
    {
        Color Color { get; }

        char Representation { get; }

        int TimespanFall { get; }
    }
}