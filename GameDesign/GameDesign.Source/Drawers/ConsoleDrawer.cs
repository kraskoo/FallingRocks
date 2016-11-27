namespace GameDesign.Source.Drawers
{
    using System;
    using Enumerations;
    using Mappers;

    public class ConsoleDrawer : BaseDrawer
    {
        private readonly Color defaultForegroundColor;
        private readonly Color defaultBackgroundColor;
        private readonly ConsoleColorMapper mapper;

        public ConsoleDrawer(
            ConsoleColorMapper mapper,
            ConsoleColor defaultForeground,
            ConsoleColor defaultBackground)
        {
            this.mapper = mapper;
            this.defaultForegroundColor =
                this.mapper.GetOuterColorConvertedToGameColor[defaultForeground];
            this.defaultBackgroundColor =
                this.mapper.GetOuterColorConvertedToGameColor[defaultBackground];
        }

        public override Color DefaultForeground => this.defaultForegroundColor;

        public override Color DefaultBackground => this.defaultBackgroundColor;

        public override void SetCursorAt(int x, int y)
        {
            Console.SetCursorPosition(x, y);
        }

        public override void SetBackground(Color color)
        {
            Console.BackgroundColor = this.mapper.GetGameColorConvertedToOuterColor[color];
        }

        public override void SetForeground(Color color)
        {
            Console.ForegroundColor = this.mapper.GetGameColorConvertedToOuterColor[color];
        }

        public override void PrepareDrawing(Color color, int x, int y)
        {
            this.SetForeground(color);
            this.SetCursorAt(x, y);
        }

        public override void Draw(Color color, int x, int y, char representation)
        {
            this.PrepareDrawing(color, x, y);
            this.DrawElement(representation);
        }

        public override void Draw(
            Color background, Color foreground, int x, int y, char representation)
        {
            this.SetBackground(background);
            this.PrepareDrawing(foreground, x, y);
            this.DrawElement(representation);
        }

        public override void DrawElement(char representation)
        {
            Console.Write(representation);
        }

        public override void ClearAt(int x, int y)
        {
            this.SetForeground(this.defaultForegroundColor);
            this.SetCursorAt(x, y);
            Console.Write('\0');
        }

        public override void Clear()
        {
            Console.Clear();
        }
    }
}