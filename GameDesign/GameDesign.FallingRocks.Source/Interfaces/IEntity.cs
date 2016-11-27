namespace GameDesign.FallingRocks.Source.Interfaces
{
    using GameDesign.Source.Interfaces;

    public interface IEntity
    {
        IPosition Position { get; }

        int Width { get; }

        int Height { get; }
    }
}