namespace GameDesign.Source.Interfaces
{
    using Enumerations;

    public interface IKeyProvider
    {
        bool HasPressedKey { get; }

        Key Key { get; }
    }
}