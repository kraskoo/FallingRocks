namespace GameDesign.FallingRocks.Source.Interfaces
{
    public interface IMenuState : IGameStateExtendable
    {
        IMenuField MenuField { get; }
    }
}