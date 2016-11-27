namespace GameDesign.Source.Interfaces
{
    using System;
    using System.Collections.Generic;
    using Enumerations;

    public interface IGameState
    {
        IDictionary<Key, Action> ActionByKey { get; }

        string NextStateName { get; }

        bool IsCurrentStateRunning { get; }

        void StartStateContext();

        void ExecuteActionOnKey(Key key);

        void ChangeState();
    }
}