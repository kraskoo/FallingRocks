namespace GameDesign.Source
{
    using Interfaces;

    public struct Position : IPosition
    {
        public Position(int x, int y) : this()
        {
            this.X = x;
            this.Y = y;
        }

        public int X { get; private set; }

        public int Y { get; private set; }

        public override bool Equals(object obj)
        {
            if (!(obj is Position))
            {
                return false;
            }

            var anotherPosition = (Position)obj;
            return this.X == anotherPosition.X && this.Y == anotherPosition.Y;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (this.X * this.Y) ^ 17;
            }
        }

        public void ChangePosition(int toPositionX, int toPositionY)
        {
            this.X = toPositionX;
            this.Y = toPositionY;
        }

        public IPosition MoveTo(int toX, int toY)
        {
            IPosition lastPosition = this;
            this.ChangePosition(toX, toY);
            return lastPosition;
        }
    }
}