using System.Windows;
using System.Windows.Input;

namespace LinscEditor.Helpers
{
    public static class TextBoxHelper
    {
        public static readonly DependencyProperty Command =
            DependencyProperty.RegisterAttached
            (
                "Command",
                typeof(ICommand),
                typeof(TextBoxHelper),
                new PropertyMetadata(null)
            );

        public static ICommand GetCommand(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(Command);
        }

        public static void SetCommand(DependencyObject obj, ICommand value)
        {
            obj.SetValue(Command, value);
        }
    }
}
