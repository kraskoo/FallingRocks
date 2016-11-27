namespace GameDesign.FallingRocks.Source.GameEntities
{
    using Common;
    using GameDesign.Source;
    using GameDesign.Source.Enumerations;
    using GameDesign.Source.Interfaces;
    using Interfaces;

    public class Player : Entity, IPlayer
    {
        private const int DefaultHeight = 1;

        public Player(
            Color color,
            IPosition position,
            string representation = EntityConstant.DefaultPlayerRepresentation,
            int lives = EntityConstant.DefaultPlayerLives,
            int score = 0) : base(position, representation.Length, Player.DefaultHeight)
        {
            this.Color = color;
            this.Representation = representation;
            this.Lives = lives;
            this.Score = score;
        }

        public Color Color { get; }

        public string Representation { get; }

        public int Lives { get; private set; }

        public int Score { get; private set; }

        public void ReactOnRockCollision(IPosition rockPosition)
        {
            bool hasHit = false;
            for (int x = this.Position.X; x < this.Position.X + this.Width; x++)
            {
                if (new Position(x, this.Position.Y).Equals(rockPosition))
                {
                    this.Lives--;
                    hasHit = true;
                    break;
                }
            }

            if (!hasHit)
            {
                this.Score++;
            }
        }
    }
}