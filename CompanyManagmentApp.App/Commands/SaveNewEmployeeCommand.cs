using CompanyManagmentApp.App.State.Authenticator;
using CompanyManagmentApp.App.State.Navigators;
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
    public class SaveNewEmployeeCommand : ICommand
    {
        private readonly INavigator navigator;
        private readonly IAuthenticator authenticator;
        private readonly IPermissionService permissionService;
        private readonly IDataService<Employees> dataService;
        private readonly EmployeesViewModel employeesViewModel;
        private readonly AddEmployeeViewModel addEmployeeViewModel;

        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }

        public SaveNewEmployeeCommand(INavigator navigator, IAuthenticator authenticator, IPermissionService permissionService, 
            IDataService<Employees> dataService, EmployeesViewModel employeesViewModel, AddEmployeeViewModel addEmployeeViewModel)
        {
            this.navigator = navigator;
            this.authenticator = authenticator;
            this.permissionService = permissionService;
            this.dataService = dataService;
            this.employeesViewModel = employeesViewModel;
            this.addEmployeeViewModel = addEmployeeViewModel;
        }

        public bool CanExecute(object parameter)
        {
            var Employee = addEmployeeViewModel.Employee;
            if(Employee.Job == null || string.IsNullOrWhiteSpace(Employee.FirstName) || string.IsNullOrWhiteSpace(Employee.LastName)
                || string.IsNullOrWhiteSpace(Employee.Email))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public async void Execute(object parameter)
        {
            Employees newEmployee = await dataService.Create(addEmployeeViewModel.Employee);
            if(await authenticator.Register(newEmployee))
            {
                var user = await authenticator.GetByEmployee(newEmployee);
                await permissionService.UpdatePermissions(new List<Permission> 
                {
                    addEmployeeViewModel.DepartmentsPermissions,
                    addEmployeeViewModel.EmployeesPermissions,
                    addEmployeeViewModel.JobsPermissions,
                    addEmployeeViewModel.ProjectsPermissions,
                    addEmployeeViewModel.WorkCardsPermissions
                }, user.Id);

                employeesViewModel.Init();
                navigator.CurrentViewModel = employeesViewModel;
            }
        }
    }
}
