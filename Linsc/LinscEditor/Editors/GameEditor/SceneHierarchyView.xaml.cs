using LinscEditor.Components;
using LinscEditor.GameProject;
using System.Windows.Controls;

namespace LinscEditor.Editors.GameEditor
{
    /// <summary>
    /// Interaction logic for SceneHierarchyControl.xaml
    /// </summary>
    public partial class SceneHierarchyView : UserControl
    {
        public SceneHierarchyView()
        {
            InitializeComponent();
        }

        private void OnAddGameEntityButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var button = (Button)sender;
            var vm = button.DataContext as Scene;

            vm.AddGameEntityCommand.Execute( new GameEntity(vm) { Name = "New Game Entity" });
        }

        private void OnSceneEntitiesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GameEntityView.Instance.DataContext = null;
            var listbox = (ListBox)sender;
            if(listbox.SelectedItems.Count > 0)
            {
                GameEntityView.Instance.DataContext = listbox.SelectedItems[0];
            }
        }
    }
}
