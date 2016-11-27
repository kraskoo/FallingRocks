namespace GameDesign.FallingRocks.Source.Interfaces
{
    public interface IPlayState : IGameStateExtendable
    {
        IPlayField PlayField { get; }
    }
}