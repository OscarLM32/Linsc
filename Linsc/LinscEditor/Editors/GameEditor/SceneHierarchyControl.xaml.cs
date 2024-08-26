using LinscEditor.GameProject;
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

namespace LinscEditor.Editors.GameEditor
{
    /// <summary>
    /// Interaction logic for SceneHierarchyControl.xaml
    /// </summary>
    public partial class SceneHierarchyControl : UserControl
    {
        public SceneHierarchyControl()
        {
            InitializeComponent();
        }

        private void OnAddSceneButton_Click(object sender, RoutedEventArgs e)
        {
            var project = DataContext as Project;
            project.AddScene("New Scene " + project.Scenes.Count);
        }
    }
}
