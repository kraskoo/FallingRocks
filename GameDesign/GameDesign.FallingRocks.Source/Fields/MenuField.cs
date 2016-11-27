namespace GameDesign.FallingRocks.Source.Fields
{ 
    using System.Collections.Generic;
    using GameDesign.Source;
    using GameDesign.Source.Common;
    using GameDesign.Source.Enumerations;
    using GameDesign.Source.Interfaces;
    using GameEntities;
    using Interfaces;

    public class MenuField : Field, IMenuField
    {
        public const Color BackgroundColor = Color.Yellow;
        public const Color ForegroundColor = Color.Red;
        public const Color MarkedBackgroundColor = Color.Gray;
        public const Color MarkedForegroundColor = Color.LightGray;
        private const int TextFieldWidth = 6;
        private const int TextFieldHeight = 3;
        private readonly IDictionary<string, ITextMenuField> menuFieldByName;
        private ITextMenuField currentHighlightCategory;

        public MenuField(
            IDrawer drawer,
            int startX,
            int startY,
            int width,
            int height,
            params MenuCategory[] menuCategories) : base(drawer, startX, startY, width, height)
        {
            this.menuFieldByName = this.InitilizeTextFields(
                menuCategories,
                MenuField.TextFieldWidth,
                MenuField.TextFieldHeight);
            this.currentHighlightCategory = this.menuFieldByName["Resume"];
        }

        public ITextMenuField CurrentSelect => this.currentHighlightCategory;

        public IEnumerable<ITextMenuField> MenuFields => this.menuFieldByName.Values;

        public void MoveOnNextPosition()
        {
            if ((int)this.currentHighlightCategory.Representation.GetMenuCategoryByName() == 4)
            {
                this.currentHighlightCategory =
                    this.menuFieldByName[((MenuCategory)1).MenuCategoryAsString()];
            }
            else
            {
                int categoryNum =
                    (int)this.currentHighlightCategory.Representation.GetMenuCategoryByName();
                categoryNum++;
                this.currentHighlightCategory =
                    this.menuFieldByName[((MenuCategory)categoryNum).MenuCategoryAsString()];
            }

            // TODO: Initialize execution plan ...
        }

        public void MoveOnPreviousPosition()
        {
            if ((int)this.currentHighlightCategory.Representation.GetMenuCategoryByName() == 1)
            {
                this.currentHighlightCategory = 
                    this.menuFieldByName[((MenuCategory)4).MenuCategoryAsString()];
            }
            else
            {
                int categoryNum =
                    (int)this.currentHighlightCategory.Representation.GetMenuCategoryByName();
                categoryNum--;
                this.currentHighlightCategory =
                    this.menuFieldByName[((MenuCategory)categoryNum).MenuCategoryAsString()];
            }

            // TODO: Initialize execution plan ...
        }

        public ITextMenuField GetMenuFieldByName(string name)
        {
            return this.menuFieldByName[name];
        }

        private IDictionary<string, ITextMenuField> InitilizeTextFields(
            MenuCategory[] menuCategories, int width, int height)
        {
            int maxLength = 0;
            string[] categories = new string[menuCategories.Length];
            for (int i = 0; i < menuCategories.Length; i++)
            {
                categories[i] = menuCategories[i].MenuCategoryAsString();
                if (categories[i].Length > maxLength)
                {
                    maxLength = categories[i].Length;
                }
            }

            int fieldWidth = this.StartX + this.Width;
            int pureMiddle = fieldWidth.HalfedValue();
            IDictionary<string, ITextMenuField> textMenuFields =
                new Dictionary<string, ITextMenuField>();
            int spaceBetween = 1;
            int nextStartYPosition = spaceBetween + 4;
            foreach (string category in categories)
            {
                int realWidth = maxLength + width + 1;
                int startXPosition = pureMiddle - realWidth.HalfedValue(false);
                int startYPosition = nextStartYPosition;
                textMenuFields[category] = new TextMenuField(
                    category,
                    new Position(startXPosition, startYPosition),
                    realWidth,
                    height);
                nextStartYPosition = startYPosition + height + spaceBetween;
            }

            return textMenuFields;
        }
    }
}