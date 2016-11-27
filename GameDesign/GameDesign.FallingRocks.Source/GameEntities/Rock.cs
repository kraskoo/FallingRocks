namespace GameDesign.FallingRocks.Source.GameEntities
{
    using GameDesign.Source.Enumerations;
    using GameDesign.Source.Interfaces;
    using Interfaces;

    public class Rock : Entity, IRock
    {
        private const int DefaultRockWidthAndHeight = 1;

        public Rock(
            Color color,
            char representation,
            IPosition position,
            int timespanFall) : base(
                position, Rock.DefaultRockWidthAndHeight, Rock.DefaultRockWidthAndHeight)
        {
            this.Color = color;
            this.Representation = representation;
            this.TimespanFall = timespanFall;
        }

        public Color Color { get; }

        public char Representation { get; }

        public int TimespanFall { get; }

        public int CompareTo(IRock other)
        {
            if (this.Position.Equals(other.Position))
            {
                return 0;
            }

            return -1;
        }
    }
}