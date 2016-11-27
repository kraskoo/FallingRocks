namespace GameDesign.FallingRocks.Source.GameStates
{
    using System;
    using System.Collections.Generic;
    using Common;
    using GameDesign.Source.Common;
    using GameDesign.Source.Enumerations;
    using Interfaces;

    public class MenuState : GameState, IMenuState
    {
        private string nextStateName;

        public MenuState(IContextExtendable gameContext, IMenuField menuField) : base(gameContext)
        {
            this.MenuField = menuField;
            this.nextStateName = typeof(MenuState).Name;
        }

        public IMenuField MenuField { get; }

        public override string NextStateName
        {
            get { return this.nextStateName; }
            protected set { this.nextStateName = value; }
        }

        public override IDictionary<Key, Action> ActionByKey => this.DefaultActionKey();

        public sealed override bool IsCurrentStateRunning { get; protected set; }

        public override void ExecuteActionOnKey(Key key)
        {
            if (this.ActionByKey.ContainsKey(key))
            {
                this.ActionByKey[key]();
            }
        }

        public override void ChangeState()
        {
            this.IsCurrentStateRunning = false;
            this.Context.Drawer.Clear();
            this.Context.ChangeState(this.NextStateName).SetPreviousState(this);
            this.Context.StartContext()();
        }

        public override void StartStateContext()
        {
            this.IsCurrentStateRunning = true;
            this.InitialDraw();
            DrawerExtension.ExecuteDraweres();
            this.GetBackCursorToSafePosition();
            while (this.IsCurrentStateRunning)
            {
                if (this.Context.KeyProvider.HasPressedKey)
                {
                    this.ExecuteActionOnKey(this.Context.KeyProvider.Key);
                }

                this.Context.Drawer.SetBackground(this.Context.Drawer.DefaultBackground);
                this.Context.Drawer.SetForeground(this.Context.Drawer.DefaultForeground);
                this.GetBackCursorToSafePosition();
            }

            this.ChangeState();
        }

        public void ConfirmChoise()
        {
            this.ManageChoiseAction();
        }

        public void BackToPrevious()
        {
            this.Context.Drawer.Clear();
            this.ChangeNextToPreviousState();
            this.IsCurrentStateRunning = false;
            this.Context.StartContext()();
        }

        private void InitialDraw()
        {
            foreach (var menuField in this.MenuField.MenuFields)
            {
                Color background =
                    this.CheckIfMenuFieldIsCurrentSelected(menuField) ? Color.White : Color.Yellow;
                this.DrawTextMenuField(
                    menuField,
                    background,
                    Color.Black);
            }
        }

        private bool CheckIfMenuFieldIsCurrentSelected(ITextMenuField menuField)
        {
            return menuField.Representation == this.MenuField.CurrentSelect.Representation;
        }

        private void DrawTextMenuField(ITextMenuField menuField, Color background, Color foreground)
        {
            this.DrawTextField(menuField, background, foreground);
        }

        private void DrawTextField(ITextMenuField menuField, Color background, Color foreground)
        {
            var fieldHeightEnd = menuField.Position.Y + menuField.Height;
            int width = menuField.Width + menuField.Position.X - 1;
            int outerWidth = (menuField.Width - menuField.Representation.Length).HalfedValue(false);
            int lowerWidthBound = menuField.Position.X + outerWidth;
            int upperWidthBound = width - outerWidth;
            for (int y = menuField.Position.Y; y < fieldHeightEnd; y++)
            {
                for (int x = menuField.Position.X; x < width; x++)
                {
                    int halfHeight = (fieldHeightEnd - y).HalfedValue();
                    int halfHeightPlus = (fieldHeightEnd - y).HalfedValue(false) - halfHeight;
                    if ((halfHeightPlus - halfHeight)
                        .IsInBounds(halfHeight, halfHeightPlus) &&
                        x > lowerWidthBound - 1  &&
                        x < upperWidthBound)
                    {
                        this.Context
                            .Drawer
                            .EnqueueDrawAction(
                                background,
                                foreground,
                                x,
                                y,
                                menuField.Representation[x - menuField.Position.X - outerWidth]);
                    }
                    else
                    {
                        this.Context.Drawer.EnqueueDrawAction(background, foreground, x, y, ' ');
                    }
                }
            }
        }

        private void ChangeNextToPreviousState()
        {
            this.Context
                .ChangeState(this.PreviousState.ToString())
                .SetPreviousState(this);
        }

        private IDictionary<Key, Action> DefaultActionKey()
        {
            return new Dictionary<Key, Action>
            {
                { Key.Down, this.MoveOnNextPosition },
                { Key.Up, this.MoveOnPreviousPosition },
                { Key.Escape, this.BackToPrevious },
                { Key.Enter, this.ConfirmChoise }
            };
        }

        private void MoveOnNextPosition()
        {
            this.DrawTextMenuField(
                this.CurrentMenuFieldName(),
                Color.Yellow,
                Color.Black);
            this.MenuField.MoveOnNextPosition();
            this.DrawTextMenuField(
                this.CurrentMenuFieldName(),
                Color.White,
                Color.Black);
            DrawerExtension.ExecuteDraweres();
        }

        private void MoveOnPreviousPosition()
        {
            this.DrawTextMenuField(
                this.CurrentMenuFieldName(),
                Color.Yellow,
                Color.Black);
            this.MenuField.MoveOnPreviousPosition();
            this.DrawTextMenuField(
                this.CurrentMenuFieldName(),
                Color.White,
                Color.Black);
            DrawerExtension.ExecuteDraweres();
        }

        private ITextMenuField CurrentMenuFieldName()
        {
            return this.MenuField
                .GetMenuFieldByName(this.MenuField.CurrentSelect.Representation);
        }

        private void ManageChoiseAction()
        {
            switch (this.MenuField.CurrentSelect.Representation.GetMenuCategoryByName())
            {
                case MenuCategory.Resume:
                    this.BackToPrevious();
                    break;
                case MenuCategory.NewGame:
                    this.Restart();
                    break;
                case MenuCategory.Options:
                    // TODO: Realization of some logic
                    break;
                case MenuCategory.Exit:
                    this.Exit();
                    break;
            }
        }

        private void Exit()
        {
            this.Context.Drawer.Clear();
            Environment.Exit(Environment.ExitCode);
        }

        private void Restart()
        {
            this.Context.Drawer.Clear();
            this.Context.Restart();
        }
    }
}