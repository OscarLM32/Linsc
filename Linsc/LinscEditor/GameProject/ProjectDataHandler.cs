using LinscEditor.Utilities;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization;

namespace LinscEditor.GameProject
{
    //TODO: think about making a new file for ProjectData
    [DataContract]
    internal class ProjectData
    {
        [DataMember]
        public string Name { get; set ; }
        [DataMember]
        public string DirPath { get; set; }

        [DataMember]
        public DateTime CreationDate { get; private set; }
        [DataMember]
        public DateTime LastAccessTime { get; set; }

        //TODO: I do not like using the fullpath as key for the searches
        public string ProjectPath { get => @$"{DirPath}\{Name}{Project.FileExtension}"; }

        public byte[] Icon { get;  set; }
        public string IconPath { get => @$"{DirPath}\.Linsc\icon.png"; }
        public byte[] Thumbnail { get;  set; }
        public string ThumbnailPath{ get => @$"{DirPath}\.Linsc\thumbnail.png"; }


        public ProjectData(string name, string path, DateTime creationDate, DateTime lastAccessTime)
        {
            Name = name;
            DirPath = path;
            CreationDate = creationDate;
            LastAccessTime = lastAccessTime;

            Icon = File.ReadAllBytes(IconPath);
            Thumbnail = File.ReadAllBytes(ThumbnailPath);
        }
    }


    internal static class ProjectDataHandler
    {
        private static readonly string _applicationDataPath = @$"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}\LincsEditor\";
        private static readonly string _projectDataPath = @$"{_applicationDataPath}ProjectData.xml";

        private static ObservableCollection<ProjectData> _projects;
        public static ReadOnlyObservableCollection<ProjectData> Projects { get; private set; }

        static ProjectDataHandler()
        {
            try
            {
                if (!Directory.Exists(_applicationDataPath))
                {
                    Directory.CreateDirectory(_applicationDataPath);
                }
                _projects = new();
                Projects = new ReadOnlyObservableCollection<ProjectData>(_projects);
                ReadProjectData();
            }
            catch (Exception ex) 
            {
                Logger.LogMessage(MessageType.ERROR, $"Failed to read project data");
                throw;
            }
        }

        public static void AddProjectData(ProjectData data)
        {
            ReadProjectData();
            var duplicatedProject = _projects.FirstOrDefault(x => x.ProjectPath == data.ProjectPath);
            if (duplicatedProject != null)
            {
                //LogError
            }
            else
            {
                _projects.Add(data);
                //Data can be lost if it is not inmediately saved
                WriteProjectData();
            }
        }
        public static void ReadProjectData()
        {
            if (File.Exists(_projectDataPath))
            {
                List<ProjectData> projectData = new();
                projectData = DCSerializer.FromFile<List<ProjectData>>(_projectDataPath);

                _projects.Clear();
                foreach (var project in projectData)
                {
                    if (File.Exists(project.ProjectPath))
                    {
                        project.Icon = File.ReadAllBytes(project.IconPath);
                        project.Thumbnail = File.ReadAllBytes(project.ThumbnailPath);

                        _projects.Add(project);
                    }
                }
            }
        }

        public static void WriteProjectData()
        {
            List<ProjectData> projectData = _projects.OrderByDescending(x => x.LastAccessTime).ToList();

            DCSerializer.ToFile(projectData, _projectDataPath);
        }
    }
}
