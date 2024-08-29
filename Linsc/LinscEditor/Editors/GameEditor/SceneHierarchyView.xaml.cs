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
    public partial class SceneHierarchyControl : UserControl
    {
        public SceneHierarchyControl()
        {
            InitializeComponent();
        }

        private void OnAddGameEntityButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var button = (Button)sender;
            var vm = button.DataContext as Scene;

            vm.AddGameEntityCommand.Execute( new GameEntity(vm) { Name = "New Game Entity" });
        }
    }
}
