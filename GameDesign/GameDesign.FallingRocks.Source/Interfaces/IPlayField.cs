namespace GameDesign.FallingRocks.Source.Interfaces
{
    using GameDesign.Source.Interfaces;

    public interface IPlayField : IField
    {
        IPlayer Player { get; }

        IRock SeedRock(int elapsedTime, int delay);

        void LeftMove();

        void RightMove();

        void FallAllRock(int elapsedTime);
    }
}