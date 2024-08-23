using LinscEditor.Editors.GameEditor;
using System.Windows;
using System.Windows.Controls;

namespace LinscEditor.GameProject
{
    /// <summary>
    /// Interaction logic for ProjectSelection.xaml
    /// </summary>
    public partial class ProjectSelectionPage : Page
    {
        private ProjectCreationPage _cachedProjectCreationPage = new ProjectCreationPage();

        public ProjectSelectionPage()
        {
            InitializeComponent();
        }

        private void NewProjectButton_Click(object sender, RoutedEventArgs e)
        {
            if (_cachedProjectCreationPage == null)
            {
                _cachedProjectCreationPage = new();
            }

            NavigationService.Navigate(_cachedProjectCreationPage);
        }

        private void OnOpenProjectButton_Click(object sender, RoutedEventArgs e)
        {
            var thisWindow = Window.GetWindow(this);
            var gameEditorWindow = new MainGameEditorWindow();

            OpenProject op = DataContext as OpenProject;
            ProjectData selectedProject = projectListBox.SelectedItem as ProjectData;

            if(selectedProject != null)
            {
                var project = op.Open(selectedProject);

                gameEditorWindow.DataContext = project;
                gameEditorWindow.Show();
                Application.Current.MainWindow = gameEditorWindow;

                thisWindow.Close();
            }
            else
            {
                //TODO: log a proper error dialog
            }
        }
    }
}
