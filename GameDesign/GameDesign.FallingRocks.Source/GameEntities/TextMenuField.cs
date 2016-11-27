namespace GameDesign.FallingRocks.Source.GameEntities
{
    using GameDesign.Source.Interfaces;
    using Interfaces;

    public class TextMenuField : Entity, ITextMenuField
    {
        public TextMenuField(
            string representation,
            IPosition position,
            int width,
            int height) : base(
                position, width, height)
        {
            this.Representation = representation;
        }
        
        public string Representation { get; }
    }
}