using CompanyManagmentApp.App.Commands;
using CompanyManagmentApp.App.State.Authenticator;
using CompanyManagmentApp.App.State.Navigators;
using CompanyManagmentApp.DataAccess.IServices;
using CompanyManagmentApp.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CompanyManagmentApp.App.ViewModels
{
    public class ProjectsViewModel : ViewModelBase
    {
        IDataService<Projects> _dataService;
        private readonly IPermissionService permissionService;
        private readonly IAuthenticator authenticator;
        INavigator _navigator;
        private Projects _selectedProject;

        public Projects SelectedProject
        {
            get { return _selectedProject; }
            set { _selectedProject = value; OnPropertyChanged(nameof(SelectedProject)); }
        }


        public ICommand EditProject { get; }
        public ICommand AddProject { get; }
        public ICommand DeleteProject { get; }

        private ObservableCollection<Projects> _projects;
        public ObservableCollection<Projects> Projects
        {
            get { return _projects; }
            set 
            { 
                _projects = value;
                OnPropertyChanged(nameof(Projects));
            }
        }

        public ProjectsViewModel(IPermissionService permissionService, IAuthenticator authenticator, INavigator navigator, IDataService<Projects> dataService)
        {
            _dataService = dataService;
            this.permissionService = permissionService;
            this.authenticator = authenticator;
            _navigator = navigator;
            Init();
            EditProject = new OpenEditProjectViewModel(this.permissionService, this.authenticator, _navigator,_dataService, this, true);
            AddProject = new OpenEditProjectViewModel(this.permissionService, this.authenticator, _navigator, _dataService, this, false);
            DeleteProject = new DeleteProjectCommand(this.permissionService, this.authenticator, _dataService, this);
        }

        public async void Init()
        {
            _projects = new ObservableCollection<Projects>(await _dataService.GetAll());
            OnPropertyChanged(nameof(Projects));
        }

    }
}
