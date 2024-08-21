using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;

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
    }
}
