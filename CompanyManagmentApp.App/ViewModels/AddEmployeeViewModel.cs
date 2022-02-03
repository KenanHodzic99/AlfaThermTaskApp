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
    public class AddEmployeeViewModel : ViewModelBase
    {
        private Employees _employee = new Employees();

        public Employees Employee
        {
            get { return _employee; }
            set { _employee = value;  OnPropertyChanged(nameof(Employee)); }
        }

        private Jobs _selectedJob = new Jobs();

        public Jobs SelectedJob
        {
            get { return _selectedJob; }
            set 
            { 
                _selectedJob = value; 
                OnPropertyChanged(nameof(SelectedJob));
                _employee.Job = value;
                _employee.JobId = value.Id;
                OnPropertyChanged(nameof(Employee));
            }
        }


        private ObservableCollection<Jobs> _jobs;

        public ObservableCollection<Jobs> Jobs
        {
            get { return _jobs; }
            set { _jobs = value; OnPropertyChanged(nameof(Jobs)); }
        }

        private Permission _projectsPermissions = new Permission();

        public Permission ProjectsPermissions
        {
            get { return _projectsPermissions; }
            set { _projectsPermissions = value; OnPropertyChanged(nameof(ProjectsPermissions)); }
        }

        private Permission _departmentsPermissions = new Permission();

        public Permission DepartmentsPermissions
        {
            get { return _departmentsPermissions; }
            set { _departmentsPermissions = value; OnPropertyChanged(nameof(DepartmentsPermissions)); }
        }

        private Permission _jobsPermissions = new Permission();

        public Permission JobsPermissions
        {
            get { return _jobsPermissions; }
            set { _jobsPermissions = value; OnPropertyChanged(nameof(JobsPermissions)); }
        }

        private Permission _workCardsPermissions = new Permission();

        public Permission WorkCardsPermissions
        {
            get { return _workCardsPermissions; }
            set { _workCardsPermissions = value; OnPropertyChanged(nameof(WorkCardsPermissions)); }
        }

        private Permission _employeesPermissions = new Permission();
        private readonly IAuthenticator authenticator;
        private readonly IPermissionService permissionService;
        private readonly IDataService<Jobs> jobsDataService;
        private readonly IDataService<Employees> dataService;
        private readonly INavigator navigator;
        private readonly EmployeesViewModel employeesViewModel;

        public Permission EmployeesPermissions
        {
            get { return _employeesPermissions; }
            set { _employeesPermissions = value; OnPropertyChanged(nameof(EmployeesPermissions)); }
        }

        public ICommand SaveChanges { get; }

        public AddEmployeeViewModel(IAuthenticator authenticator, IPermissionService permissionService,
            IDataService<Jobs> jobsDataService ,IDataService<Employees> dataService, 
            INavigator navigator, EmployeesViewModel employeesViewModel)
        {
            this.authenticator = authenticator;
            this.permissionService = permissionService;
            this.jobsDataService = jobsDataService;
            this.dataService = dataService;
            this.navigator = navigator;
            this.employeesViewModel = employeesViewModel;
            Init();
            SaveChanges = new SaveNewEmployeeCommand(navigator,authenticator,permissionService,dataService,employeesViewModel,this);
        }

        public async void Init()
        {
            this.Jobs = new ObservableCollection<Jobs>(await jobsDataService.GetAll());

            this._jobsPermissions.ModuleName = "Jobs";
            OnPropertyChanged(nameof(JobsPermissions));
            this._projectsPermissions.ModuleName = "Project";
            OnPropertyChanged(nameof(ProjectsPermissions));
            this._workCardsPermissions.ModuleName = "Work card";
            OnPropertyChanged(nameof(WorkCardsPermissions));
            this._employeesPermissions.ModuleName = "Employee";
            OnPropertyChanged(nameof(EmployeesPermissions));
            this._departmentsPermissions.ModuleName = "Department";
            OnPropertyChanged(nameof(DepartmentsPermissions));
        }
    }
}
