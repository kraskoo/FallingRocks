namespace GameDesign.Source.Interfaces
{
    public interface IField
    {
        IDrawer Drawer { get; }

        int StartX { get; }

        int StartY { get; }

        int Width { get; }

        int Height { get; }

        int EndBoundLeftFieldSide { get; }

        int EndBoundRightFieldSide { get; }
    }
}