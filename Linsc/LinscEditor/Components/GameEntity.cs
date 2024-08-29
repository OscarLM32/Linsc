

using LinscEditor.GameProject;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace LinscEditor.Components
{
    [DataContract]
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

        [DataMember]
        public Scene ParentScene { get; private set; }

        [DataMember(Name = "Components")]
        private readonly ObservableCollection<Component> _components = new();
        public ReadOnlyObservableCollection<Component> Components { get; }


        public GameEntity(Scene parentScene)
        {
            Debug.Assert(parentScene != null);
            ParentScene = parentScene;

            Components = new(_components);
        }
    }
}
