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
    public class SaveNewEmployeeCommand : ICommand
    {
        private readonly INavigator navigator;
        private readonly IAuthenticator authenticator;
        private readonly IPermissionService permissionService;
        private readonly IDataService<Employees> dataService;
        private readonly EmployeesViewModel employeesViewModel;
        private readonly AddEmployeeViewModel addEmployeeViewModel;

        public event EventHandler CanExecuteChanged;

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
            return true;
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
