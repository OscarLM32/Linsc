

using LinscEditor.Common;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.Serialization;

namespace LinscEditor.GameProject
{
    [DataContract(Name = "GameProject")]
    internal class Project : ViewModelBase
    {
        public static string FileExtension { get; } = ".linsc";

        [DataMember]
        public string Name { get; private set; }
        [DataMember]
        public string Path { get; private set; }
        public string FullPath { get => $"{Path}{Name}"; }

        [DataMember(Name = "Scenes")]
        private ObservableCollection<Scene> _scenes = new();
        public ReadOnlyObservableCollection<Scene> Scenes { get; }

        public Project(string name, string path)
        {
            Name = name;
            Path = path;

            _scenes.Add(new Scene("Default Scene", this));
        }
    }
}
