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
using System.Windows.Shapes;

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
