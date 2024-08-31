using LinscEditor.GameProject;
using System.Windows;

namespace LinscEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += OnMainWindowLoaded;
        }

        private void OnMainWindowLoaded(object sender, RoutedEventArgs e)
        {
            Loaded -= OnMainWindowLoaded;
            OpenProjectSelectionPage();
        }

        private void OpenProjectSelectionPage()
        {
            var projectSelectionPage = new ProjectSelectionPage();
            MainFrame.Navigate(projectSelectionPage);
        }
    }
}