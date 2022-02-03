using CompanyManagmentApp.App.State.Authenticator;
using CompanyManagmentApp.App.ViewModels;
using CompanyManagmentApp.DataAccess.IServices;
using CompanyManagmentApp.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CompanyManagmentApp.App.Commands
{
    public class DeleteEmployeeCommand : ICommand
    {
        private readonly IPermissionService permissionService;
        private readonly IAuthenticator authenticator;
        private readonly IDataService<Employees> dataService;
        private readonly EmployeesViewModel employeeViewModel;
        private Permission _loadedPermissions;

        public event EventHandler CanExecuteChanged;

        public DeleteEmployeeCommand(IPermissionService permissionService, IAuthenticator authenticator, IDataService<Employees> dataService, EmployeesViewModel employeeViewModel)
        {
            this.permissionService = permissionService;
            this.authenticator = authenticator;
            this.dataService = dataService;
            this.employeeViewModel = employeeViewModel;
        }
        public bool CanExecute(object parameter)
        {
            Task<Permission> task = Task.Run<Permission>(async () => await permissionService.GetPermissionForModule("Employee", authenticator.CurrentUser.Id));
            _loadedPermissions = task.Result;
            if ((bool)_loadedPermissions.CanDelete)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async void Execute(object parameter)
        {
            var Employee = (Employees)parameter;
            await dataService.Delete(Employee.Id);
            employeeViewModel.Init();
        }
    }
}
