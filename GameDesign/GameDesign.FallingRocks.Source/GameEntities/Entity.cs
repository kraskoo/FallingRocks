namespace GameDesign.FallingRocks.Source.GameEntities
{
    using GameDesign.Source.Interfaces;
    using Interfaces;

    public abstract class Entity : IEntity
    {
        protected Entity(IPosition position, int width, int height)
        {
            this.Position = position;
            this.Width = width;
            this.Height = height;
        }

        public IPosition Position { get; }

        public int Width { get; }

        public int Height { get; }
    }
}