using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LinscEditor.Utilities
{
    /// <summary>
    /// Interaction logic for LoggerView.xaml
    /// </summary>
    public partial class LoggerView : UserControl
    {
        public LoggerView()
        {
            InitializeComponent();

            Loaded += (s, e) =>
            {
                Logger.LogMessage(MessageType.INFO, "Info message");
                Logger.LogMessage(MessageType.WARNING, "Warning message");
                Logger.LogMessage(MessageType.ERROR, "Error message");
            };
        }
    }
}
