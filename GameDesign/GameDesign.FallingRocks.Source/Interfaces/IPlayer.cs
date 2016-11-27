namespace GameDesign.FallingRocks.Source.Interfaces
{
    using GameDesign.Source.Enumerations;
    using GameDesign.Source.Interfaces;

    public interface IPlayer : IEntity
    {
        Color Color { get; }

        string Representation { get; }

        int Lives { get; }

        int Score { get; }

        void ReactOnRockCollision(IPosition rockPosition);
    }
}