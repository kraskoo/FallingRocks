namespace GameDesign.FallingRocks.Source.Fields
{
    using System.Collections.Generic;
    using Common;
    using GameDesign.Source;
    using GameDesign.Source.Common;
    using GameDesign.Source.Interfaces;
    using GameDesign.Source.Providers;
    using GameEntities;
    using Interfaces;

    public class PlayField : Field, IPlayField
    {
        private const int LowerBoundFallChance = 10;
        private const int UpperBoundFallChance = 35;
        private readonly IList<IRock> rocksOnField;
        private readonly int playerLength;
        private bool isLastSideRockSeedIsLeft;

        public PlayField(
            IDrawer drawer,
            IPlayer player,
            int startX,
            int startY,
            int width,
            int height) : base(drawer, startX, startY, width, height)
        {
            this.Player = player;
            this.playerLength = this.Player.Representation.Length;
            this.rocksOnField = new List<IRock>();
            this.isLastSideRockSeedIsLeft = true;
        }

        public IPlayer Player { get; }

        public void LeftMove()
        {
            var coditionCheck = this.Player.Position.X - 1 > this.StartX;
            if (!coditionCheck)
            {
                return;
            }

            var previousPlayerPosition = this.Player
                    .Position
                    .MoveTo(this.Player.Position.X - 1, this.Player.Position.Y);
            previousPlayerPosition
                .ChangePosition(
                    previousPlayerPosition.X + this.Player.Representation.Length - 1,
                    previousPlayerPosition.Y);
            this.Drawer.EnqueueCleanAction(
                previousPlayerPosition.X,
                previousPlayerPosition.Y,
                true);
            for (int i = 0; i < this.playerLength; i++)
            {
                this.Drawer.EnqueueDrawAction(
                    this.Player.Color,
                    this.Player.Position.X + i,
                    this.Player.Position.Y,
                    this.Player.Representation[i],
                    true);
            }
        }

        public void RightMove()
        {
            var condtionCheck =
                this.Player.Position.X + this.playerLength < this.StartX + this.Width;
            if (!condtionCheck)
            {
                return;
            }

            var previousPlayerPosition = this.Player
                    .Position
                    .MoveTo(
                        this.Player.Position.X + 1,
                        this.Player.Position.Y);
            this.Drawer.EnqueueCleanAction(
                previousPlayerPosition.X,
                previousPlayerPosition.Y,
                true);
            for (int i = 0; i < this.playerLength; i++)
            {
                this.Drawer.EnqueueDrawAction(
                    this.Player.Color,
                    this.Player.Position.X + i,
                    this.Player.Position.Y,
                    this.Player.Representation[i],
                    true);
            }
        }

        public IRock SeedRock(int elapsedTime, int delay)
        {
            if (elapsedTime % delay == 0)
            {
                int fallChance = RandomProvider.Next(1, 101);
                if (fallChance.IsInBounds(LowerBoundFallChance, UpperBoundFallChance))
                {
                    int colorIndex = RandomProvider.Next(0, ColorProvider.Colors.Length);
                    int formIndex = RandomProvider.Next(0, EntityConstant.DefaultRockRepresentations.Length);
                    int x = this.isLastSideRockSeedIsLeft ?
                        RandomProvider.Next(this.StartX + 1, this.EndBoundLeftFieldSide) :
                        RandomProvider.Next(this.EndBoundLeftFieldSide + 1, this.EndBoundRightFieldSide);
                    int fallTime = RandomProvider.Next(4700, 6400);
                    this.ChangeLastRockSide();
                    this.rocksOnField.Add(new Rock(
                            ColorProvider.Colors[colorIndex],
                            EntityConstant.DefaultRockRepresentations[formIndex],
                            new Position(x, this.StartY),
                            fallTime));
                    return this.rocksOnField[this.rocksOnField.Count - 1];
                }
            }

            return null;
        }

        public void FallAllRock(int elapsedTime)
        {
            for (int i = 0; i < this.rocksOnField.Count; i++)
            {
                if (elapsedTime % this.rocksOnField[i].TimespanFall == 0)
                {
                    this.Fall(this.rocksOnField[i]);
                    if (this.rocksOnField[i].Position.Y == this.Height + this.StartY)
                    {
                        this.Player.ReactOnRockCollision(this.rocksOnField[i].Position);
                        this.rocksOnField.RemoveAt(i);
                    }
                    else
                    {
                        this.Drawer.EnqueueDrawAction(
                            this.rocksOnField[i].Color,
                            this.rocksOnField[i].Position.X,
                            this.rocksOnField[i].Position.Y,
                            this.rocksOnField[i].Representation);
                    }
                }
            }
        }

        private void ChangeLastRockSide()
        {
            this.isLastSideRockSeedIsLeft = !this.isLastSideRockSeedIsLeft;
        }

        private void Fall(IRock rock)
        {
            rock.Position
                .ChangePosition(rock.Position.X, rock.Position.Y + 1);
            this.Drawer.EnqueueCleanAction(rock.Position.X, rock.Position.Y - 1);
        }
    }
}