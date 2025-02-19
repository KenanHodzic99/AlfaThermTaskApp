﻿using CompanyManagmentApp.App.State.Authenticator;
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
    public class SaveEmployeeCommand : ICommand
    {
        private readonly IAuthenticator authenticator;
        private readonly IPermissionService permissionService;
        private readonly IDataService<Jobs> jobsDataService;
        private readonly IDataService<Employees> dataService;
        private readonly INavigator navigator;
        private readonly EmployeesViewModel employeesViewModel;
        private readonly EditEmployeeViewModel editEmployeeViewModel;

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
            var employee = editEmployeeViewModel.Employee;
            if(employee.Job != null)
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
