using LinscEditor.Extensions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace LinscEditor.Dictionaries
{
    public partial class ControlTemplates : ResourceDictionary
    {
        private void OnTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            var textBox = sender as TextBox;
            var exp = textBox.GetBindingExpression(TextBox.TextProperty);
            if (exp == null) return;

            if (e.Key == Key.Enter)
            {
                ICommand command = textBox.GetCommand();

                if(command != null && command.CanExecute(textBox.Text))
                {
                    command.Execute(textBox.Text);
                }
                else
                {
                    exp.UpdateSource();
                }
            }
            else if (e.Key == Key.Escape)
            {
                exp.UpdateTarget();
                Keyboard.ClearFocus();
            }
        }
    }
}
