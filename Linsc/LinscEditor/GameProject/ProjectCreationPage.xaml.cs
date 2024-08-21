using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace LinscEditor.GameProject
{
    /// <summary>
    /// Interaction logic for ProjectCreation.xaml
    /// </summary>
    public partial class ProjectCreationPage : Page
    {
        public ProjectCreationPage()
        {
            InitializeComponent();
        }

        private void OnCancelButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void OnCreateButton_Click(object sender, RoutedEventArgs e)
        {
            NewProject newProject = DataContext as NewProject;
            string path = newProject.CreateProject(projectTemplateListBox.SelectedItem as ProjectTemplate);

            if (string.IsNullOrEmpty(path))
            {
                //TODO: log a proper error dialog
                Debug.WriteLine("There was an error creating the project");
            }
            else
            {
                ProjectData newProjectData = new ProjectData(newProject.ProjectName, @$"{newProject.ProjectPath}{newProject.ProjectName}", DateTime.Now, DateTime.Now);
                ProjectDataHandler.AddProjectData(newProjectData);

                NavigationService.GoBack();
            }
        }
    }
}
