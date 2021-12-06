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
    public class SaveEmployeeCommand : ICommand
    {
        private readonly IAuthenticator authenticator;
        private readonly IPermissionService permissionService;
        private readonly IDataService<Jobs> jobsDataService;
        private readonly IDataService<Employees> dataService;
        private readonly INavigator navigator;
        private readonly EmployeesViewModel employeesViewModel;
        private readonly EditEmployeeViewModel editEmployeeViewModel;

        public event EventHandler CanExecuteChanged;

        public SaveEmployeeCommand(IAuthenticator authenticator, IPermissionService permissionService, IDataService<Employees> dataService,
            INavigator navigator, EmployeesViewModel employeesViewModel, EditEmployeeViewModel editEmployeeViewModel)
        {
            this.authenticator = authenticator;
            this.permissionService = permissionService;
            this.dataService = dataService;
            this.navigator = navigator;
            this.employeesViewModel = employeesViewModel;
            this.editEmployeeViewModel = editEmployeeViewModel;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public async void Execute(object parameter)
        {
            var employee = editEmployeeViewModel.Employee;
            await dataService.Update(employee.Id,employee);

            var employeeAccount = await authenticator.GetByEmployee(employee);
            await permissionService.UpdatePermissions(new List<Permission>
                {
                    editEmployeeViewModel.DepartmentsPermissions,
                    editEmployeeViewModel.EmployeesPermissions,
                    editEmployeeViewModel.JobsPermissions,
                    editEmployeeViewModel.ProjectsPermissions,
                    editEmployeeViewModel.WorkCardsPermissions
                }, employeeAccount.Id);

            employeesViewModel.Init();
            navigator.CurrentViewModel = employeesViewModel;
        }
    }
}
