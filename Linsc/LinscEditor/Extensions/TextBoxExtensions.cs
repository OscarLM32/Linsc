using LinscEditor.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace LinscEditor.Extensions
{
    public static class TextBoxExtensions
    {
        public static ICommand GetCommand(this TextBox instance)
        {
            return TextBoxHelper.GetCommand(instance);
        }
    }
}
