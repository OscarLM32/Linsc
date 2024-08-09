using LinscEditor.Common;

namespace LinscEditor.GameProject
{
    public class NewProject : ViewModelBase
    {
        private string _name = "NewProject";
        public string Name
        {
            get => _name;
            set
            {
                if(_name != value)
                {
                    _name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }

        private string _path = @$"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\LinscProjects\";
        public string Path
        {
            get => _path;
            set
            {
                if (_path != value)
                {
                    _path = value;
                    OnPropertyChanged(nameof(Path));
                }
            }
        }
    }
}
