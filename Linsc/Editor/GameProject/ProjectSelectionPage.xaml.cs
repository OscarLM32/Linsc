﻿using System;
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

namespace Editor.GameProject
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
            NavigationService.Navigate(_cachedProjectCreationPage);
        }
    }
}
