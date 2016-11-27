namespace GameDesign.FallingRocks.Source.Interfaces
{
    using System.Collections.Generic;

    public interface IMenuField
    {
        IEnumerable<ITextMenuField> MenuFields { get; }

        ITextMenuField CurrentSelect { get; }

        void MoveOnNextPosition();

        void MoveOnPreviousPosition();

        ITextMenuField GetMenuFieldByName(string name);
    }
}