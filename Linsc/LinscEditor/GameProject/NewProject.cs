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
                    ValidateData();
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
                    ValidateData();
                    OnPropertyChanged(nameof(ProjectPath));
                }
            }
        }

        private bool _isValid = false;
        public bool IsValid
        {
            get => _isValid;
            set
            {
                if(_isValid != value)
                {
                    _isValid = value;
                    OnPropertyChanged(nameof(IsValid));
                }
            }
        }

        private string _errorMsg;
        public string ErrorMsg
        {
            get => _errorMsg;
            set
            {
                if(_errorMsg != value)
                {
                    _errorMsg = value;
                    OnPropertyChanged(nameof(ErrorMsg));
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
                ValidateData();
            }
            catch (Exception ex)
            {
                //TODO: properly show an error dialog window
                Debug.WriteLine(ex.Message);
            }
        }

        private void ValidateData()
        {
            if (!Path.EndsInDirectorySeparator(ProjectPath))
            {
                ProjectPath += @"\";
            }

            IsValid = false;
            if (string.IsNullOrWhiteSpace(ProjectName.Trim()))
            {
                ErrorMsg = "The name of the project cannot be empty.";
            }
            else if(ProjectName.IndexOfAny(Path.GetInvalidFileNameChars()) != -1)
            {
                ErrorMsg = "The project name constains invalid characters.";
            }
            //TODO: think on deleting this. The path is never empty since the path gets a "\" added at the beginning of the method
            else if(string.IsNullOrWhiteSpace(ProjectPath.Trim()))
            {
                ErrorMsg = "The path of the project cannot be empty.";
            }
            else if (ProjectPath.IndexOfAny(Path.GetInvalidPathChars()) != -1)
            {
                ErrorMsg = "The path of the project contains invalid characters.";
            }
            else if(Directory.Exists(ProjectPath) && Directory.EnumerateFileSystemEntries(ProjectPath).Any())
            {
                ErrorMsg = "The selected directory already exists and is not empty.";
            }
            else
            {
                IsValid = true;
                ErrorMsg = string.Empty;
            }
        }
    }
}
