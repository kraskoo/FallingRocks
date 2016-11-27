namespace GameDesign.Source.Interfaces
{
    using System;
    
    public interface IGameContext
    {
        IKeyProvider KeyProvider { get; }

        IDrawer Drawer { get; }

        Action StartContext();
    }
}