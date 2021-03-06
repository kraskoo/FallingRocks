﻿namespace GameDesign.FallingRocks.Source.GameStates
{
    using System;
    using System.Collections.Generic;
    using Common;
    using GameDesign.Source.Enumerations;
    using Interfaces;

    public class PlayState : GameState, IPlayState
    {
        private const int WallPadding = 4;
        private const int NumbersLength = WallPadding + 2;
        private const int DefaultFramePerTwoSecond = 8000;
        private int currentPlayerScores;
        private int currentPlayerLives;
        private int playerScoreX;
        private int playerLivesX;
        private int currentFrames;
        private string nextStateName;

        public PlayState(
            IContextExtendable gameContext,
            IPlayField playField) : base(gameContext)
        {
            this.PlayField = playField;
            this.currentPlayerScores = this.PlayField.Player.Score;
            this.currentPlayerLives = this.PlayField.Player.Lives;
            this.currentFrames = 0;
            this.nextStateName = typeof(MenuState).Name;
        }

        public IPlayField PlayField { get; }

        public override string NextStateName
        {
            get { return this.nextStateName; }
            protected set { this.nextStateName = value; }
        }

        public sealed override bool IsCurrentStateRunning { get; protected set; }

        public override IDictionary<Key, Action> ActionByKey =>
            this.DefaultActionsKey();

        public override void ExecuteActionOnKey(Key key)
        {
            if (this.ActionByKey.ContainsKey(key))
            {
                this.ActionByKey[key]();
            }
        }

        public override void StartStateContext()
        {
            this.IsCurrentStateRunning = true;
            this.InitialDraw();
            this.DrawPlayerAndScore();
            this.ExecuteDrawers();
            this.ExecuteCleaners();
            while (this.IsCurrentStateRunning)
            {
                this.ExecuteDrawers(true);
                this.ExecuteCleaners(true);
                this.currentFrames++;
                if (this.currentFrames > DefaultFramePerTwoSecond)
                {
                    this.currentFrames = 0;
                }

                this.GetBackCursorToSafePosition();
                
                while (this.Context.KeyProvider.HasPressedKey)
                {
                    this.ExecuteActionOnKey(this.Context.KeyProvider.Key);
                }
                
                this.PlayField.FallAllRock(this.currentFrames);
                IRock seededRock = this.PlayField.SeedRock(this.currentFrames, 3500);
                if (seededRock != null)
                {
                    this.DrawSeededRock(seededRock);
                }

                this.UpdatePlayerStatus();
                this.ExecuteCleaners();
                this.ExecuteDrawers();
            }

            this.ChangeState();
        }

        public override void ChangeState()
        {
            this.IsCurrentStateRunning = false;
            this.ExecuteDrawers();
            this.ExecuteDrawers(true);
            this.Context.Drawer.Clear();
            this.Context.UpdateBeforeChangeState(this.PlayField, this.PlayField.Player);
            this.Context.ChangeState(this.NextStateName).SetPreviousState(this);
            this.Context.StartContext()();
        }

        private void DrawSeededRock(IRock seedRock)
        {
            this.Context.Drawer.EnqueueDrawAction(
                seedRock.Color,
                seedRock.Position.X,
                seedRock.Position.Y,
                seedRock.Representation);
        }

        private IDictionary<Key, Action> DefaultActionsKey()
        {
            return new Dictionary<Key, Action>
            {
                { Key.Left, this.PlayField.LeftMove },
                { Key.Right, this.PlayField.RightMove },
                { Key.Escape, this.ChangeState }
            };
        }

        private void InitialDraw()
        {
            this.Context.Drawer.SetBackground(this.Context.Drawer.DefaultBackground);
            this.Context.Drawer.SetForeground(this.Context.Drawer.DefaultForeground);
            var realPlayFieldWidth = this.PlayField.Width + this.PlayField.StartX;
            var realPlayFieldHeight = this.PlayField.Height + this.PlayField.StartY;

            for (int x = this.PlayField.StartX + 1; x < realPlayFieldWidth; x++)
            {
                this.Context.Drawer.EnqueueDrawAction(
                    Color.LightGray, x, this.PlayField.StartY - 1, '_');
            }

            for (int y = this.PlayField.StartY; y < realPlayFieldHeight; y++)
            {
                this.Context.Drawer.EnqueueDrawAction(
                    Color.LightGray, this.PlayField.StartX, y, '|');
                this.Context.Drawer.EnqueueDrawAction(
                    Color.LightGray, realPlayFieldWidth, y, '|');
            }
        }

        private void DrawPlayerAndScore()
        {
            for (int i = 0; i < this.PlayField.Player.Representation.Length; i++)
            {
                this.Context.Drawer.EnqueueDrawAction(
                    this.PlayField.Player.Color,
                    this.PlayField.Player.Position.X + i,
                    this.PlayField.Player.Position.Y,
                    this.PlayField.Player.Representation[i],
                    true);
            }

            string scoreRow = $"Scores: ";
            string liveRow = $"Lives: ";
            int startX = this.PlayField.StartX + this.PlayField.Width + WallPadding;
            int startY = this.PlayField.StartY;
            this.playerScoreX = scoreRow.Length + startX;
            this.playerLivesX = liveRow.Length + startX;
            this.DrawResultPart(startX, this.playerScoreX, startY, scoreRow, Color.LightGray);
            this.DrawResultPart(startX, this.playerLivesX, startY + 1, liveRow, Color.LightGray);
            scoreRow = $"{this.PlayField.Player.Score}";
            liveRow = $"{this.PlayField.Player.Lives}";
            int scoreStart = this.playerScoreX;
            int liveStart = this.playerLivesX;
            int scoreLength = scoreStart + $"{this.PlayField.Player.Score}".Length;
            int liveRowLength = liveStart + $"{this.PlayField.Player.Lives}".Length;
            this.DrawResultPart(scoreStart, scoreLength, startY, scoreRow, Color.White);
            this.DrawResultPart(liveStart, liveRowLength, startY + 1, liveRow, Color.White);
        }

        private void DrawResultPart(
            int startX,
            int length,
            int startY,
            string representation,
            Color color)
        {
            for (int x = startX; x < length; x++)
            {
                this.Context.Drawer.EnqueueDrawAction(
                    color,
                    x,
                    startY,
                    representation[x - startX],
                    true);
            }
        }

        private void UpdatePlayerStatus()
        {
            int startY = this.PlayField.StartY;
            if (this.PlayField.Player.Score != this.currentPlayerScores)
            {
                this.currentPlayerScores = this.PlayField.Player.Score;
                int startX = this.playerScoreX;
                int length = startX + NumbersLength;
                string score = this.currentPlayerScores.ToString();
                for (int x = startX; x < length; x++)
                {
                    this.Context.Drawer.EnqueueDrawAction(Color.Black, x, startY, ' ', true);
                    if (x < startX + score.Length)
                    {
                        this.Context.Drawer.EnqueueDrawAction(
                        Color.White,
                        x,
                        startY,
                        score[x - startX],
                        true);
                    }
                }
            }

            startY++;
            if (this.PlayField.Player.Lives != this.currentPlayerLives)
            {
                this.currentPlayerLives = this.PlayField.Player.Lives;
                int startX = this.playerLivesX;
                int length = startX + NumbersLength;
                string lives = this.currentPlayerLives.ToString();
                for (int x = startX; x < length; x++)
                {
                    this.Context.Drawer.EnqueueDrawAction(Color.Black, x, startY, ' ', true);
                    if (x < startX + lives.Length)
                    {
                        this.Context.Drawer.EnqueueDrawAction(
                        Color.White,
                        x,
                        startY,
                        lives[x - startX],
                        true);
                    }
                }
            }
        }

        private void ExecuteCleaners(bool usedByPlayer = false)
        {
            DrawerExtension.ExecuteCleaneres(usedByPlayer);
        }

        private void ExecuteDrawers(bool usedByPlayer = false)
        {
            DrawerExtension.ExecuteDraweres(usedByPlayer);
        }
    }
}