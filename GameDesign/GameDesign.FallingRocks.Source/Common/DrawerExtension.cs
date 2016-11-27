namespace GameDesign.FallingRocks.Source.Common
{
    using System;
    using System.Collections.Generic;
    using GameDesign.Source.Enumerations;
    using GameDesign.Source.Interfaces;

    public static class DrawerExtension
    {
        private static readonly Func<Queue<Action>, bool> QueueIsEmpty = queue => queue.Count == 0;

        private static readonly Action<int, int, Action<int, int>> Clean = (x, y, a) => a(x, y);

        private static readonly Action<Color, int, int, char, Action<Color, int, int, char>> Draw =
            (c, x, y, r, m) => m(c, x, y, r);

        private static readonly Action<Color,
            Color, int, int, char, Action<Color, Color, int, int, char>> DrawWithBackground =
            (bc, fc, x, y, r, m) => m(bc, fc, x, y, r);

        private static readonly Queue<Action>[] DrawerActions = new Queue<Action>[2];

        private static readonly Queue<Action>[] CleanerActions = new Queue<Action>[2];

        private static readonly Queue<Action> DrawerActionsWithBackground = new Queue<Action>();

        public static void ExecuteDraweres(bool usedByPlayerOrResults = false)
        {
            if (usedByPlayerOrResults)
            {
                while (!QueueIsEmpty(DrawerActions[1]))
                {
                    lock (DrawerActions[1])
                    {
                        ExecuteQueueAction(DrawerActions, 1);
                    }
                }
            }
            else
            {
                while (!QueueIsEmpty(DrawerActions[0]))
                {
                    ExecuteQueueAction(DrawerActions, 0);
                }

                while (!QueueIsEmpty(DrawerActionsWithBackground))
                {
                    ExecuteQueueAction(DrawerActionsWithBackground);
                }
            }
        }

        public static void ExecuteCleaneres(bool usedByPlayerOrResults = false)
        {
            if (usedByPlayerOrResults)
            {
                while (!QueueIsEmpty(CleanerActions[1]))
                {
                    lock (CleanerActions[1])
                    {
                        ExecuteQueueAction(CleanerActions, 1);
                    }
                }
            }
            else
            {
                while (!QueueIsEmpty(CleanerActions[0]))
                {
                    ExecuteQueueAction(CleanerActions, 0);
                }
            }
        }

        public static void EnqueueDrawAction(
            this IDrawer drawer,
            Color color,
            int x,
            int y,
            char representation,
            bool usedByPlayerOrResults = false)
        {
            if (DrawerActions[0] == null || DrawerActions[1] == null)
            {
                lock (DrawerActions)
                {
                    lock (CleanerActions)
                    {
                        Initialize();
                    }
                }
            }

            if (usedByPlayerOrResults)
            {
                lock (DrawerActions[1])
                {
                    DrawerActions[1]?.Enqueue(
                       () => Draw(color, x, y, representation, drawer.Draw));
                }
            }
            else
            {
                DrawerActions[0]?.Enqueue(
                   () => Draw(color, x, y, representation, drawer.Draw));
            }
        }

        public static void EnqueueDrawAction(
            this IDrawer drawer,
            Color foregroundColor,
            Color backgroundColor,
            int x,
            int y,
            char representation)
        {
            if (DrawerActions[0] == null && DrawerActions[1] == null)
            {
                lock (DrawerActions)
                {
                    lock (CleanerActions)
                    {
                        Initialize();
                    }
                }
            }

            DrawerActions[0]?.Enqueue(
                () => DrawWithBackground(
                    foregroundColor, backgroundColor, x, y, representation, drawer.Draw));
        }

        public static void EnqueueCleanAction(
            this IDrawer drawer,
            int x,
            int y,
            bool usedByPlayerOrResults = false)
        {
            if (CleanerActions[0] == null || CleanerActions[1] == null)
            {
                lock (DrawerActions)
                {
                    lock (CleanerActions)
                    {
                        Initialize();
                    }
                }
            }

            if (usedByPlayerOrResults)
            {
                lock (CleanerActions[1])
                {
                    CleanerActions[1]?.Enqueue(() => Clean(x, y, drawer.ClearAt));
                }
            }
            else
            {
                CleanerActions[0]?.Enqueue(() => Clean(x, y, drawer.ClearAt));
            }
        }

        private static void Initialize()
        {
            for (int i = 0; i < CleanerActions.Length; i++)
            {
                DrawerActions[i] = new Queue<Action>();
                CleanerActions[i] = new Queue<Action>();
            }
        }

        private static void ExecuteQueueAction(Queue<Action>[] queues, int index)
        {
            if (queues[index].Count > 0)
            {
                queues[index].Dequeue()();
            }
        }

        private static void ExecuteQueueAction(Queue<Action> queues)
        {
            if (queues.Count > 0)
            {
                queues.Dequeue()();
            }
        }
    }
}