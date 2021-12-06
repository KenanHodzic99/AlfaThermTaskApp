using AlfaThermTaskApp.App.Commands;
using AlfaThermTaskApp.App.State.Authenticator;
using AlfaThermTaskApp.App.State.Navigators;
using AlfaThermTaskApp.DataAccess.IServices;
using AlfaThermTaskApp.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AlfaThermTaskApp.App.ViewModels
{
    public class EmployeesViewModel : ViewModelBase
    {
        private string _searchParameter = string.Empty;

        public string SearchParameters
        {
            get { return _searchParameter; }
            set { _searchParameter = value; OnPropertyChanged(nameof(SearchParameters)); Init(); }
        }

        private ObservableCollection<Employees> _employees;
        private readonly IDataService<Employees> dataService;
        private readonly INavigator navigator;
        private readonly IAuthenticator authenticator;
        private readonly IPermissionService permissionService;
        private readonly IDataService<Jobs> jobsDataService;

        public ObservableCollection<Employees> Employees
        {
            get { return _employees; }
            set { _employees = value; OnPropertyChanged(nameof(Employees)); }
        }

        public ICommand AddEmployee { get; }
        public ICommand EditEmployee { get; }
        public ICommand DeleteEmployee { get; }

        public EmployeesViewModel(IDataService<Employees> dataService, INavigator navigator, IAuthenticator authenticator, 
            IPermissionService permissionService, IDataService<Jobs> jobsDataService)
        {
            this.dataService = dataService;
            this.navigator = navigator;
            this.authenticator = authenticator;
            this.permissionService = permissionService;
            this.jobsDataService = jobsDataService;
            Init();
            DeleteEmployee = new DeleteEmployeeCommand(this.permissionService, this.authenticator, this.dataService, this);
            EditEmployee = new OpenEditEmployeeViewModel(this.authenticator, this.permissionService, this.jobsDataService, this.dataService, this.navigator, this);
            AddEmployee = new OpenAddEmployeeViewModel(this.authenticator, this.permissionService, this.jobsDataService, this.dataService, this.navigator, this);
        }

        public async void Init()
        {
            Employees = new ObservableCollection<Employees>(await dataService.GetAll(SearchParameters));
            OnPropertyChanged(nameof(Employees));
        }

    }
}
