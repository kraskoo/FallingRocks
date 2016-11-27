namespace GameDesign.Source.Drawers
{
    using Enumerations;
    using Interfaces;

    public abstract class BaseDrawer : IDrawer
    {
        public abstract Color DefaultForeground { get; }

        public abstract Color DefaultBackground { get; }

        public abstract void SetCursorAt(int x, int y);

        public abstract void SetForeground(Color color);

        public abstract void SetBackground(Color color);

        public abstract void Draw(Color color, int x, int y, char representation);

        public abstract void Draw(
            Color background, Color foreground, int x, int y, char representation);

        public abstract void PrepareDrawing(Color color, int x, int y);

        public abstract void DrawElement(char representation);

        public abstract void ClearAt(int x, int y);

        public abstract void Clear();
    }
}