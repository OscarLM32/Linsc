using LinscEditor.Components;
using LinscEditor.GameProject;
using LinscEditor.Utilities;
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
            var listbox = (ListBox)sender; 
            var newSelection = listbox.SelectedItems.Cast<GameEntity>().ToList();
            var previousSelection = newSelection.Except(e.AddedItems.Cast<GameEntity>()).Concat(e.RemovedItems.Cast<GameEntity>()).ToList();
            AddselectionUndoRedoActions(listbox, newSelection, previousSelection);

            MSGameEntity msEntity = null;
            if (newSelection.Any())
            {
                msEntity = new(newSelection);
            }
            GameEntityView.Instance.DataContext = msEntity;
        }

        private void AddselectionUndoRedoActions(ListBox listbox, List<GameEntity> newSelection, List<GameEntity> previousSelection)
        {

            Project.UndoRedo.Add(new UndoRedoAction
            (
                () =>
                {
                    listbox.SelectedItems.Clear();
                    foreach(var item in previousSelection)
                    {
                        listbox.SelectedItems.Add(item);
                    }
                },
                () =>
                {
                    listbox.SelectedItems.Clear();
                    foreach (var item in newSelection)
                    {
                        listbox.SelectedItems.Add(item);
                    }
                },
                "Item selection changed"
            ));
        }
    }
}
