using AlfaThermTaskApp.App.State.Authenticator;
using AlfaThermTaskApp.App.State.Navigators;
using AlfaThermTaskApp.App.ViewModels;
using AlfaThermTaskApp.DataAccess.IServices;
using AlfaThermTaskApp.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AlfaThermTaskApp.App.Commands
{
    public class OpenEditEmployeeViewModel : ICommand
    {
        private readonly IAuthenticator authenticator;
        private readonly IPermissionService permissionService;
        private readonly IDataService<Jobs> jobsDataService;
        private readonly IDataService<Employees> dataService;
        private readonly INavigator navigator;
        private readonly EmployeesViewModel employeesViewModel;
        private Permission _loadedPermissions;

        public event EventHandler CanExecuteChanged;

        public OpenEditEmployeeViewModel(IAuthenticator authenticator, IPermissionService permissionService, 
            IDataService<Jobs> jobsDataService, IDataService<Employees> dataService, INavigator navigator, EmployeesViewModel employeesViewModel)
        {
            this.authenticator = authenticator;
            this.permissionService = permissionService;
            this.jobsDataService = jobsDataService;
            this.dataService = dataService;
            this.navigator = navigator;
            this.employeesViewModel = employeesViewModel;
        }

        public bool CanExecute(object parameter)
        {
            Task<Permission> task = Task.Run<Permission>(async () => await permissionService.GetPermissionForModule("Employee", authenticator.CurrentUser.Id));
            _loadedPermissions = task.Result;
            if ((bool)_loadedPermissions.CanEdit)
               {
                return true;
               }
            else
               {
                return false;
               }
        }

        public void Execute(object parameter)
        {
            navigator.CurrentViewModel = new EditEmployeeViewModel(authenticator,permissionService,jobsDataService,dataService,navigator,employeesViewModel,(Employees)parameter);
        }
    }
}
