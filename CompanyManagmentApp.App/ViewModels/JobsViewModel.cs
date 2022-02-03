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
    public class JobsViewModel : ViewModelBase
    {
        private Jobs _selectedJob;

        public Jobs SelectedJob
        {
            get { return _selectedJob; }
            set { _selectedJob = value; OnPropertyChanged(nameof(SelectedJob)); }
        }

        private ObservableCollection<Jobs> _jobs;
        private readonly IPermissionService permissionService;
        private readonly IAuthenticator authenticator;
        private readonly IDepartmentService departmentService;
        private readonly IDataService<Jobs> dataService;
        private readonly INavigator navigator;

        public ObservableCollection<Jobs> Jobs
        {
            get { return _jobs; }
            set { _jobs = value; OnPropertyChanged(nameof(Jobs)); }
        }

        public ICommand AddJob { get; }
        public ICommand EditJob { get; }
        public ICommand DeleteJob { get; }

        public JobsViewModel(IPermissionService permissionService, IAuthenticator authenticator, IDepartmentService departmentService, IDataService<Jobs> dataService, INavigator navigator)
        {
            this.permissionService = permissionService;
            this.authenticator = authenticator;
            this.departmentService = departmentService;
            this.dataService = dataService;
            this.navigator = navigator;
            Init();
            AddJob = new OpenEditJobViewModel(this.permissionService, this.authenticator , this.navigator, this.dataService, this.departmentService, this, false);
            EditJob = new OpenEditJobViewModel(this.permissionService, this.authenticator, this.navigator, this.dataService, this.departmentService, this, true);
            DeleteJob = new DeleteJobCommand(this.permissionService, this.authenticator, this.dataService, this);
        }

        public async void Init()
        {
            Jobs = new ObservableCollection<Jobs>(await dataService.GetAll());

            OnPropertyChanged(nameof(Jobs));
        }

    }
}
