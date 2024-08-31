using LinscEditor.GameProject;
using System.Windows;

namespace LinscEditor.Editors.GameEditor
{
    /// <summary>
    /// Interaction logic for MainGameEditorWindow.xaml
    /// </summary>
    public partial class MainGameEditorWindow : Window
    {
        public MainGameEditorWindow()
        {
            InitializeComponent();
            
            Closed += OnMainWindowClosed;
        }
        private void OnMainWindowClosed(object? sender, EventArgs e)
        {
            Closed -= OnMainWindowClosed;
            Project.Current?.Unload();
        }
    }
}
