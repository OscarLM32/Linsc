using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Data;

namespace LinscEditor.Utilities
{
    [Flags]
    public enum MessageType
    {
        NONE = 0,
        
        INFO,
        WARNING,
        ERROR,

        EVERYTHING = INFO | WARNING | ERROR 
    }

    public class LogMessage
    {

        public DateTime DateTime { get; }
        public MessageType MessageType { get; }
        public string Message { get; }
        public string File { get; }
        public string Caller { get; }
        public int Line { get; }
        public string Metadata => $"{File}: {Caller} ({Line})";

        public LogMessage(MessageType messageType, string message, string file, string caller, int line)
        {
            DateTime = DateTime.Now;
            MessageType = messageType;
            Message = message;
            File = Path.GetFileName(file);
            Caller = caller;
            Line = line;
        }
    }

   public static class Logger
    {
        private static int _messageFilter = (int)MessageType.EVERYTHING;

        private readonly static ObservableCollection<LogMessage> _messages = new();
        public static ReadOnlyObservableCollection<LogMessage> Messages { get; } = new(_messages);
        public static CollectionViewSource FilteredMessages { get; } = new() { Source = Messages};

        static Logger()
        {
            FilteredMessages.Filter += (s, e) =>
            {
                var type = (int)(e.Item as LogMessage).MessageType;
                e.Accepted = (type & _messageFilter) != 0;
            };
        }

        public static async void LogMessage(MessageType type, string message, 
                                            [CallerFilePath]string file="", [CallerMemberName]string caller="", [CallerLineNumber]int line=0)
        {
            await Application.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                _messages.Add(new LogMessage(type, message, file, caller, line));
            }));
        }

        public static async void Clear()
        {
            await Application.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                _messages.Clear();
            }));
        }

        public static async void FilterMessages(int mask)
        {
            _messageFilter = mask;
            FilteredMessages.View.Refresh();
        }

    }
}
