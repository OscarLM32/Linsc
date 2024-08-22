

using LinscEditor.Common;
using LinscEditor.Utilities;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization;
using System.Windows;

namespace LinscEditor.GameProject
{
    [DataContract(Name = "GameProject")]
    internal class Project : ViewModelBase
    {
        public static Project Current => Application.Current.MainWindow.DataContext as Project;

        public static string FileExtension { get; } = ".linsc";

        [DataMember]
        public string Name { get; private set; }
        [DataMember]
        public string Path { get; private set; }
        public string FullPath { get => $"{Path}{Name}"; }

        [DataMember(Name = "Scenes")]
        private ObservableCollection<Scene> _scenes = new();
        public ReadOnlyObservableCollection<Scene> Scenes { get; private set; }


        private Scene _activeScene;
        public Scene ActiveScene
        {
            get => _activeScene;
            set
            {
                if (_activeScene != value)
                {
                    _activeScene = value;
                    OnPropertyChanged(nameof(ActiveScene));
                }
            }
        }


        public Project(string name, string path)
        {
            Name = name;
            Path = path;

            OnDeserialized(new StreamingContext());
        }

        public static Project Load(string file)
        {
            Debug.Assert(File.Exists(file));
            return DCSerializer.FromFile<Project>(file);
        }

        [OnDeserialized]
        //TODO: I do not thnik the streaming context is necessary
        private void OnDeserialized(StreamingContext context) 
        {
            if(_scenes != null)
            {
                Scenes = new ReadOnlyObservableCollection<Scene>(_scenes);
                OnPropertyChanged(nameof(Scenes));
            }
            ActiveScene = Scenes.FirstOrDefault(x => x.IsActive);
        }

        public void Unload()
        {

        }

        public static void Save(Project project)
        {
            DCSerializer.ToFile(project, project.FullPath);
        }
    }
}
