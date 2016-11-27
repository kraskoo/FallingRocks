namespace GameDesign.Source.Interfaces
{
    /// <summary>
    /// This object provide two points for orientation on coordinate system.
    /// </summary>
    public interface IPosition
    {
        int X { get; }

        int Y { get; }

        IPosition MoveTo(int toPositionX, int toPositionY);

        void ChangePosition(int newX, int newY);
    }
}