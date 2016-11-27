namespace GameDesign.FallingRocks.Source.Interfaces
{
    using System.Collections.Generic;
    using GameDesign.Source.Interfaces;

    public interface IContextExtendable : IGameContext
    {
        IPosition LastCursorPosition { get; }

        IDictionary<string, IGameStateExtendable> StateByName { get; }

        IGameStateExtendable ChangeState(string nameOfState);

        void UpdateBeforeChangeState(IPlayField playField, IPlayer player);
    }
}