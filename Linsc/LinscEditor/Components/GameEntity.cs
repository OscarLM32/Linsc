

using LinscEditor.GameProject;
using LinscEditor.Utilities;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Windows.Input;

namespace LinscEditor.Components
{
    [DataContract]
    [KnownType(typeof(Transform))]
    public class GameEntity : ViewModelBase
    {
        private string _name = "New GameEntity";
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
        public ICommand RenameCommand { get; private set; }

        private bool _isEnabled = true;
        [DataMember]
        public bool IsEnabled
        {
            get => _isEnabled;
            set
            {
                if (_isEnabled != value)
                {
                    _isEnabled = value;
                    OnPropertyChanged(nameof(IsEnabled));
                }
            }
        }
        public ICommand IsEnableCommand { get; private set; }

        [DataMember]
        public Scene ParentScene { get; private set; }

        [DataMember(Name = "Components")]
        private readonly ObservableCollection<Component> _components = new();
        public ReadOnlyObservableCollection<Component> Components { get; private set; }


        public GameEntity(Scene parentScene)
        {
            Debug.Assert(parentScene != null);
            ParentScene = parentScene;

            _components.Add(new Transform(this));
            OnDeserialized(new StreamingContext());
        }

        [OnDeserialized]
        private void OnDeserialized(StreamingContext context)
        {
            if(_components != null)
            {
                Components = new(_components);
                OnPropertyChanged(nameof(Components));
            }

            RenameCommand = new RelayCommand<string>
            (
                newName =>
                {
                    string oldName = Name;
                    Name = newName;

                    Project.UndoRedo.Add(new UndoRedoAction(nameof(Name), this, oldName, newName, $"Rename {oldName} to {newName}"));
                },

                newName =>
                {
                    bool ret = true;

                    ret = ret && !string.IsNullOrEmpty(newName);
                    ret = ret && (_name != newName);
                    return ret;
                }
            );

            IsEnableCommand = new RelayCommand<bool>
            (
                value =>
                {
                    IsEnabled = value;

                    Project.UndoRedo.Add(new UndoRedoAction(nameof(IsEnabled), this, !value, value, $"Enable/Disable {Name}"));
                }
            );
        }
    }
}
