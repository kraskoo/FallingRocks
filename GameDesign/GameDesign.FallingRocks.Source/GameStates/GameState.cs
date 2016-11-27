namespace GameDesign.FallingRocks.Source.GameStates
{
    using System;
    using System.Collections.Generic;
    using GameDesign.Source;
    using GameDesign.Source.Enumerations;
    using GameDesign.Source.Interfaces;
    using Interfaces;

    public abstract class GameState : IGameStateExtendable
    {
        private readonly IPosition cursorSafePosition;

        protected GameState(IContextExtendable gameContext)
        {
            this.Context = gameContext;
            this.cursorSafePosition = new Position(1, 1);
        }

        public IContextExtendable Context { get; }

        public IGameStateExtendable PreviousState { get; private set; }

        public abstract string NextStateName { get; protected set; }

        public abstract bool IsCurrentStateRunning { get; protected set; }

        public abstract IDictionary<Key, Action> ActionByKey { get; }

        public abstract void ExecuteActionOnKey(Key key);

        public abstract void StartStateContext();

        public abstract void ChangeState();

        public void SetPreviousState(IGameStateExtendable previousState)
        {
            this.PreviousState = previousState;
        }

        public override string ToString()
        {
            return this.GetType().Name;
        }

        protected void GetBackCursorToSafePosition()
        {
            this.Context
                .Drawer
                .SetCursorAt(this.cursorSafePosition.X, this.cursorSafePosition.Y);
        }
    }
}