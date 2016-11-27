namespace GameDesign.FallingRocks.Source.Game
{
    using System;
    using System.Collections.Generic;
    using Fields;
    using GameDesign.Source.Interfaces;
    using GameStates;
    using Interfaces;

    public class GameContext : IContextExtendable
    {
        private readonly int widowWidth;
        private readonly int windowHeight;
        private IGameStateExtendable currentState;
        private IPlayField playField;
        private IPlayer player;

        public GameContext(
            IKeyProvider keyProvider,
            IDrawer drawer,
            int windowWidth,
            int windowHeight)
        {
            this.KeyProvider = keyProvider;
            this.Drawer = drawer;
            this.widowWidth = windowWidth;
            this.windowHeight = windowHeight;
        }

        public IKeyProvider KeyProvider { get; }

        public IDrawer Drawer { get; }

        public IDictionary<string, IGameStateExtendable> StateByName =>
            new Dictionary<string, IGameStateExtendable>
            {
                {
                    nameof(PlayState), new PlayState(this, this.playField)
                },
                {
                    nameof(MenuState),
                    new MenuState(
                        this,
                        new MenuField(
                            this.Drawer,
                            0,
                            0,
                            this.widowWidth,
                            this.windowHeight,
                            GameDesign.Source.Enumerations.MenuCategory.Resume,
                            GameDesign.Source.Enumerations.MenuCategory.NewGame,
                            GameDesign.Source.Enumerations.MenuCategory.Options,
                            GameDesign.Source.Enumerations.MenuCategory.Exit))
                }
            };

        public void Initialize(
            IGameStateExtendable startState,
            IPlayField useablePlayField,
            IPlayer contextPlayer)
        {
            this.currentState = startState;
            this.playField = useablePlayField;
            this.player = contextPlayer;
        }

        public Action StartContext()
        {
            return this.currentState.StartStateContext;
        }

        public IGameStateExtendable ChangeState(string nameOfState)
        {
            var state = this.StateByName[nameOfState];
            this.currentState = state;
            return state;
        }

        public void UpdateBeforeChangeState(IPlayField updatePlayField, IPlayer updatePlayer)
        {
            this.playField = updatePlayField;
            this.player = updatePlayer;
        }
    }
}