using CompanyManagmentApp.App.Commands;
using CompanyManagmentApp.App.State.Navigators;
using CompanyManagmentApp.DataAccess.IServices;
using CompanyManagmentApp.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CompanyManagmentApp.App.ViewModels
{
    public class EditProjectViewModel : ViewModelBase
    {
        private readonly INavigator navigator;
        private readonly ProjectsViewModel projectsViewModel;
        IDataService<Projects> _dataService;
        public ICommand SaveProject { get; }
        private Projects _project = new Projects();
        public Projects Project
        {
            get { return _project; }
            set { _project = value; OnPropertyChanged(nameof(Project)); }
        }

        private string _projectStatus;

        public string ProjectStatus
        {
            get { return _projectStatus; }
            set { 
                _projectStatus = value; 
                OnPropertyChanged(nameof(ProjectStatus)); 
                _project.ProjectStatus = value; 
                OnPropertyChanged(nameof(Project)); 
            }
        }

        public EditProjectViewModel(INavigator navigator, ProjectsViewModel projectsViewModel, IDataService<Projects> dataService)
        {
            this.navigator = navigator;
            this.projectsViewModel = projectsViewModel;
            _dataService = dataService;
            SaveProject = new SaveProjectCommand(this,_dataService, this.projectsViewModel, this.navigator);
        }

        public EditProjectViewModel(INavigator navigator, ProjectsViewModel projectsViewModel, IDataService<Projects> dataService, Projects project)
        {
            this.navigator = navigator;
            this.projectsViewModel = projectsViewModel;
            _dataService = dataService;
            Project = project;
            ProjectStatus = project.ProjectStatus;
            SaveProject = new SaveProjectCommand(this, _dataService, this.projectsViewModel, this.navigator);
        }




    }
}
