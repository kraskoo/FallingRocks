namespace GameDesign.FallingRocks.Source.Game
{
    using System;
    using Fields;
    using GameDesign.Source;
    using GameDesign.Source.Drawers;
    using GameDesign.Source.Enumerations;
    using GameDesign.Source.InputHandlers;
    using GameDesign.Source.Interfaces;
    using GameDesign.Source.Mappers;
    using GameEntities;
    using GameStates;
    using Interfaces;

    public class EngineInitializer : IEngineInitializer
    {
        private const int DefaultFieldStartX = 0;
        private const int DefaultFieldStartY = 4;
        private static readonly int WindowWidth = Console.WindowWidth;
        private static readonly int WindowHeight = Console.WindowHeight;
        private static readonly int DefaultFieldWidth = WindowWidth - 20;
        private static readonly int DefaultFieldHeight = WindowHeight - 5;
        private readonly FallingRocksEngine fallingRocksEngine;
        private int fieldStartX;
        private int fieldStartY;
        private int fieldWidth;
        private int fieldHeight;

        public EngineInitializer(FallingRocksEngine fallingRocksEngine)
        {
            this.fallingRocksEngine = fallingRocksEngine;
            Console.CursorVisible = false;
        }

        public void ResolveEngineDependencies()
        {
            var player = this.InitializePlayer();
            IKeyProvider keyProvider = new ConsoleInputHandler();
            var drawer = this.InitializeDrawer();
            var gameContext = this.InitializeGame(keyProvider, drawer, player);
            this.fallingRocksEngine.Initialize(gameContext);
        }

        private IPlayer InitializePlayer()
        {
            this.fieldStartX = DefaultFieldStartX;
            this.fieldStartY = DefaultFieldStartY;
            this.fieldWidth = DefaultFieldWidth;
            this.fieldHeight = DefaultFieldHeight;
            var inFieldWidth = this.fieldStartX + this.fieldWidth;
            var inFieldMiddleWidth = inFieldWidth % 2 == 0 ?
                inFieldWidth / 2 : (inFieldWidth / 2) - 1;
            var playerPosition = new Position(
                inFieldMiddleWidth, this.fieldStartY + this.fieldHeight);
            IPlayer player = new Player(Color.Green, playerPosition);
            return player;
        }

        private IDrawer InitializeDrawer()
        {
            var consoleColorMapper = new ConsoleColorMapper();
            var background = Console.BackgroundColor;
            var foreground = Console.ForegroundColor;
            IDrawer drawer = new ConsoleDrawer(consoleColorMapper, foreground, background);
            return drawer;
        }

        private IContextExtendable InitializeGame(
            IKeyProvider keyProvider, IDrawer drawer, IPlayer player)
        {
            var gameContext = new GameContext(
                keyProvider, drawer, WindowWidth, WindowHeight);
            IPlayField playField = new PlayField(
                drawer,
                player,
                this.fieldStartX,
                this.fieldStartY,
                this.fieldWidth,
                this.fieldHeight);
            IGameStateExtendable playGameState = new PlayState(gameContext, playField);
            gameContext.Initialize(playGameState, playField, player);
            return gameContext;
        }
    }
}