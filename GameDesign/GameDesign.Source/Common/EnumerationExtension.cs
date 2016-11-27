namespace GameDesign.Source.Common
{
    using System;
    using Enumerations;

    public static class EnumerationExtension
    {
        public static string MenuCategoryAsString(this MenuCategory menuCategory)
        {
            return MenuCategoryString(menuCategory);
        }

        public static MenuCategory GetMenuCategoryByName(this string categoryName)
        {
            switch (categoryName)
            {
                case "Resume":
                    return MenuCategory.Resume;
                case "New Game":
                    return MenuCategory.NewGame;
                case "Options":
                    return MenuCategory.Options;
                case "Exit":
                    return MenuCategory.Exit;
                default:
                    throw new InvalidCastException("Unknown category name.");
            }
        }

        private static string MenuCategoryString(MenuCategory category)
        {
            switch (category)
            {
                case MenuCategory.Resume:
                    return "Resume";
                case MenuCategory.NewGame:
                    return "New Game";
                case MenuCategory.Options:
                    return "Options";
                case MenuCategory.Exit:
                    return "Exit";
                default:
                    throw new InvalidCastException("Unknown type.");
            }
        }
    }
}