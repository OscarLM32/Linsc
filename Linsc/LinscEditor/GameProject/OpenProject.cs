using System.Collections.ObjectModel;

namespace LinscEditor.GameProject
{
    internal class OpenProject : ViewModelBase
    {
        //Ref to projectData so the data can be binded
        public ReadOnlyObservableCollection<ProjectData> ProjectData { get; } = ProjectDataHandler.Projects;

        public Project Open(ProjectData data)
        {
            //Update the project data stores in this instance in case some other instance of the editor has modified it
            ProjectDataHandler.ReadProjectData();
            var projectData = ProjectData.FirstOrDefault(x => x.ProjectPath == data.ProjectPath);
            if (projectData != null)
            {
                projectData.LastAccessTime = DateTime.Now;
                ProjectDataHandler.WriteProjectData();
            }
            else
            {
                //TODO: Log a proper error
            }

            return Project.Load(projectData.ProjectPath);
        }
    }
}
