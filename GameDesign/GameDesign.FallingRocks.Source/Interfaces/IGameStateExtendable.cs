namespace GameDesign.FallingRocks.Source.Interfaces
{
    using GameDesign.Source.Interfaces;

    public interface IGameStateExtendable : IGameState
    {
        IContextExtendable Context { get; }

        IGameStateExtendable PreviousState { get; }

        void SetPreviousState(IGameStateExtendable previousState);
    }
}