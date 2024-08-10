using LinscEditor.Common;
using LinscEditor.Utilities;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;

namespace LinscEditor.GameProject
{
    public class NewProject : ViewModelBase
    {
        //TODO: get the path from the installation location
        private const string _projectTemplatesPath = @"..\..\LinscEditor\ProjectTemplates";

        private ObservableCollection<ProjectTemplate> _projectTemplates = new();
        public ReadOnlyObservableCollection<ProjectTemplate> ProjectTemplates { get; }

        private string _projectName = "NewProject";
        public string ProjectName
        {
            get => _projectName;
            set
            {
                if(_projectName != value)
                {
                    _projectName = value;
                    OnPropertyChanged(nameof(ProjectName));
                }
            }
        }

        private string _projectPath = @$"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\LinscProjects\";
        public string ProjectPath
        {
            get => _projectPath;
            set
            {
                if (_projectPath != value)
                {
                    _projectPath = value;
                    OnPropertyChanged(nameof(ProjectPath));
                }
            }
        }

        public NewProject()
        {
            try
            {
                string[] templateFiles = Directory.GetFiles(_projectTemplatesPath, "template.xml", SearchOption.AllDirectories);
                Debug.Assert(templateFiles.Any());

                foreach (string file in templateFiles) 
                {
                    ProjectTemplate template = Serializer.FromFile<ProjectTemplate>(file);

                    template.IconFilePath = Path.GetFullPath(Path.Combine(Path.GetDirectoryName(file), "icon.png"));
                    template.Icon = File.ReadAllBytes(template.IconFilePath);

                    template.ThumbnailFilePath= Path.GetFullPath(Path.Combine(Path.GetDirectoryName(file), "thumbnail.png"));
                    template.Thumbnail = File.ReadAllBytes(template.ThumbnailFilePath);

                    template.ProjectFilePath = Path.GetFullPath(Path.Combine(Path.GetDirectoryName(file), template.ProjectFile));

                    _projectTemplates.Add(template);
                }

                ProjectTemplates = new(_projectTemplates);
            }
            catch (Exception ex)
            {
                //TODO: properly show an error dialog window
                Debug.WriteLine(ex.Message);
            }


        }
    }
}
