namespace GameDesign.Source.Interfaces
{
    using Enumerations;

    public interface IDrawer
    {
        Color DefaultForeground { get; }

        Color DefaultBackground { get; }

        void SetCursorAt(int x, int y);

        void SetForeground(Color color);

        void SetBackground(Color color);

        void PrepareDrawing(Color color, int x, int y);

        void DrawElement(char representation);

        void ClearAt(int x, int y);

        void Draw(Color color, int x, int y, char representation);

        void Draw(
            Color background, Color foreground, int x, int y, char representation);

        void Clear();
    }
}