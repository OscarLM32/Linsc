using LinscEditor.Utilities;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization;
using System.Windows;
using System.Windows.Input;

namespace LinscEditor.GameProject
{
    [DataContract(Name = "GameProject")]
    public class Project : ViewModelBase
    {
        public static Project Current => Application.Current.MainWindow.DataContext as Project;

        public static string FileExtension { get; } = ".linsc";

        public static UndoRedo UndoRedo { get; private set; } = new();
        public ICommand UndoCommand { get; private set; }
        public ICommand RedoCommand { get; private set; }

        [DataMember]
        public string Name { get; private set; }
        [DataMember]
        public string Path { get; private set; }
        public string FullPath { get => $@"{Path}{Name}\{Name}{FileExtension}"; }

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

        public void Unload()
        {
            UndoRedo.Reset();
        }

        public ICommand SaveCommand { get; private set; }

        public static void Save(Project project)
        {
            DCSerializer.ToFile(project, project.FullPath);
            Logger.LogMessage(MessageType.INFO, $"The project has been saved to {project.FullPath}");
        }

        [OnDeserialized]
        private void OnDeserialized(StreamingContext context)
        {
            if (_scenes != null)
            {
                Scenes = new ReadOnlyObservableCollection<Scene>(_scenes);
                OnPropertyChanged(nameof(Scenes));
            }
            ActiveScene = Scenes.FirstOrDefault(x => x.IsActive);

            AddSceneCommand = new RelayCommand<object>(
                x =>
                {
                    AddScene($"New Scene {_scenes.Count}");
                    Scene newScene = _scenes.Last();
                    int newSceneIndex = _scenes.Count - 1;

                    UndoRedo.Add(new UndoRedoAction
                    (
                        () => RemoveScene(newScene),
                        () => _scenes.Insert(newSceneIndex, newScene),
                        $"Add scene {newScene.Name}"
                    ));
                }
            );

            RemoveSceneCommand = new RelayCommand<Scene>(
                scene =>
                {
                    int sceneIndex = Scenes.IndexOf(scene);
                    RemoveScene(scene);

                    UndoRedo.Add(new UndoRedoAction
                    (
                        () => _scenes.Insert(sceneIndex, scene),
                        () => RemoveScene(scene),
                        $"Remove scene {scene.Name}"
                    ));
                },

                scene =>
                {
                    return !scene.IsActive;
                }
            );

            UndoCommand = new RelayCommand<object>((x) => UndoRedo.Undo());
            RedoCommand = new RelayCommand<object>((x) => UndoRedo.Redo());

            SaveCommand = new RelayCommand<object>((x) => Save(this));
        }

        public ICommand AddSceneCommand { get; private set; }
        public ICommand RemoveSceneCommand { get; private set; }

        private void AddScene(string sceneName)
        {
            Debug.Assert(!string.IsNullOrEmpty(sceneName.Trim()));
            _scenes.Add(new Scene(sceneName, this));
        }

        private void RemoveScene(Scene scene) 
        {
            Debug.Assert(Scenes.Contains(scene));
            _scenes.Remove(scene);
        }
    }
}
