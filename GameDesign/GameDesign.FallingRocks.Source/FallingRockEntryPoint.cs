namespace GameDesign.FallingRocks.Source
{
    using Game;
    using Interfaces;

    public class FallingRockEntryPoint
    {
        public static void Main()
        {
            var engine = new FallingRocksEngine();
            IEngineInitializer engineInitializer = new EngineInitializer(engine);
            engineInitializer.ResolveEngineDependencies();
            engine.StartGame();
        }
    }
}