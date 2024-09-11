using System.Windows;
using System.Windows.Controls;

namespace LinscEditor.Utilities
{
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

        private void OnClearButton_Click(object sender, RoutedEventArgs e)
        {
            Logger.Clear();
        }

        private void OnMessageFilter_ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            var filterMask = MessageType.NONE;
            if(toggleInfo.IsChecked == true) filterMask |= MessageType.INFO;
            if(toggleWarning.IsChecked == true) filterMask |= MessageType.WARNING;
            if(toggleError.IsChecked == true) filterMask |= MessageType.ERROR;

            Logger.SetMessageFilter((int)filterMask);
        }
    }
}
