using System.Windows.Input;

namespace Lab2Var2
{
    public static class CustomCommands
    {
        public static readonly RoutedUICommand MeasuredData = new
            (
                "MeasuredData",
                "MeasuredData",
                typeof(CustomCommands),
                new InputGestureCollection()
                {
                    new KeyGesture(Key.D1, ModifierKeys.Control)
                }
            );

        public static readonly RoutedUICommand Splines = new
            (
                "Splines",
                "Splines",
                typeof(CustomCommands),
                new InputGestureCollection()
                {
                    new KeyGesture(Key.D2, ModifierKeys.Control)
                }
            );
    }
}
