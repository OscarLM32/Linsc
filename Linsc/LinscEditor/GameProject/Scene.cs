using LinscEditor.Components;
using LinscEditor.Utilities;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Windows.Input;

namespace LinscEditor.GameProject
{
    [DataContract]
    public class Scene : ViewModelBase
    {

        private string _name;
        [DataMember]
        public string Name
        {
            get => _name;
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }

        [DataMember]
        public Project Project { get; private set; }

        [DataMember]

        private bool _isActive;
        [DataMember]
        public bool IsActive
        {
            get => _isActive;
            set
            {
                if (_isActive != value)
                {
                    _isActive = value;
                    OnPropertyChanged(nameof(IsActive));
                }
            }
        }

        [DataMember(Name = "Entities")]
        private ObservableCollection<GameEntity> _entities = new();
        public ReadOnlyObservableCollection<GameEntity> Entities { get; private set; }

        public Scene(string name, Project project)
        {
            Debug.Assert(project != null);

            Name = name;
            Project = project;

            OnDeserialized(new StreamingContext());
        }

        [OnDeserialized]
        private void OnDeserialized(StreamingContext context)
        {
            if (_entities != null)
            {
                Entities = new ReadOnlyObservableCollection<GameEntity>(_entities);
                OnPropertyChanged(nameof(Entities));
            }

            AddGameEntityCommand = new RelayCommand<GameEntity>(
                entity =>
                {
                    AddGameEntityInternal(entity);
                    int index = _entities.Count - 1;

                    Project.UndoRedo.Add(new UndoRedoAction
                    (
                        () => RemoveGameEntityInternal(entity),
                        () => _entities.Insert(index, entity),
                        $"Add entity {entity.Name} to scene {Name}"
                    ));
                }
            );

            RemoveGameEntityCommand = new RelayCommand<GameEntity>(
                entity =>
                {
                    int index = _entities.IndexOf(entity);
                    RemoveGameEntityInternal(entity);

                    Project.UndoRedo.Add(new UndoRedoAction
                    (
                        () => _entities.Insert(index, entity),
                        () => RemoveGameEntityInternal(entity),
                        $"Remove entity {entity.Name} from scene {Name}"
                    ));
                }
            );
        }

        public ICommand AddGameEntityCommand { get; private set; }
        public ICommand RemoveGameEntityCommand { get; private set; }
        private void AddGameEntityInternal(GameEntity entity)
        {
            Debug.Assert(!_entities.Contains(entity));
            _entities.Add(entity);
        }

        private void RemoveGameEntityInternal(GameEntity entity)
        {
            _entities.Remove(entity);
        }
    }
}