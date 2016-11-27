namespace GameDesign.Source
{
    using Common;
    using Interfaces;

    public class Field : IField
    {
        protected Field(
            IDrawer drawer,
            int startX,
            int startY,
            int width,
            int height)
        {
            this.Drawer = drawer;
            this.StartX = startX;
            this.StartY = startY;
            this.Width = width;
            this.Height = height;
            this.EndBoundLeftFieldSide = (startX + width).HalfedValue();
            this.EndBoundRightFieldSide = width;
        }

        public IDrawer Drawer { get; }

        public int StartX { get; }

        public int StartY { get; }

        public int Width { get; }

        public int Height { get; }

        public int EndBoundLeftFieldSide { get; }

        public int EndBoundRightFieldSide { get; }
    }
}