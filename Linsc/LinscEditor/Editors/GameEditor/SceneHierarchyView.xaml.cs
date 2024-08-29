using LinscEditor.Components;
using LinscEditor.GameProject;
using System;
using System.Collections.Generic;
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
            var selectedItem = (sender as ListBox).SelectedItems[0];
            GameEntityView.Instance.DataContext = selectedItem;
        }
    }
}
