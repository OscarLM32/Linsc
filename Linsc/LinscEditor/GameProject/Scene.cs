using LinscEditor.Components;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Runtime.Serialization;

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
        public ReadOnlyObservableCollection<GameEntity> Entities { get; }



        public Scene(string name, Project project)
        {
            Debug.Assert(project != null);

            Name = name;
            Project = project;
        }
    }
}