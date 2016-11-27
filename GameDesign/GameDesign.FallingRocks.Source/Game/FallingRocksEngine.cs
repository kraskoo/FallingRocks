namespace GameDesign.FallingRocks.Source.Game
{
    using GameDesign.Source.Interfaces;
    using Interfaces;

    public class FallingRocksEngine : IRunnable
    {
        private IContextExtendable gameContext;

        public void StartGame()
        {
            this.gameContext.StartContext()();
        }

        public void Initialize(IContextExtendable context)
        {
            this.gameContext = context;
        }
    }
}