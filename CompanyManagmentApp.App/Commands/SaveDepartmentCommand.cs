using CompanyManagmentApp.App.State.Navigators;
using CompanyManagmentApp.App.ViewModels;
using CompanyManagmentApp.DataAccess.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CompanyManagmentApp.App.Commands
{
    public class SaveDepartmentCommand : ICommand
    {
        private readonly IDepartmentService departmentService;
        private readonly DepartmentsViewModel departmentsViewModel;
        private readonly INavigator navigator;
        private readonly EditDepartmentViewModel editDepartmentViewModel;
        private readonly bool isRoot;
        private readonly bool isEdit;

        public event EventHandler CanExecuteChanged;

        public SaveDepartmentCommand(IDepartmentService departmentService, DepartmentsViewModel departmentsViewModel, INavigator navigator, EditDepartmentViewModel editDepartmentViewModel, bool isRoot, bool isEdit)
        {
            this.departmentService = departmentService;
            this.departmentsViewModel = departmentsViewModel;
            this.navigator = navigator;
            this.editDepartmentViewModel = editDepartmentViewModel;
            this.isRoot = isRoot;
            this.isEdit = isEdit;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public async void Execute(object parameter)
        {
            if (isRoot)
            {
                await departmentService.CreateRoot(editDepartmentViewModel.Department);
                departmentsViewModel.Init();
                navigator.CurrentViewModel = departmentsViewModel;
            }
            else
            {
                if (isEdit)
                {
                    await departmentService.Update(editDepartmentViewModel.Department.Id, editDepartmentViewModel.Department);
                    departmentsViewModel.Init();
                    navigator.CurrentViewModel = departmentsViewModel;
                }
                else
                {
                    await departmentService.Create(editDepartmentViewModel.Department, editDepartmentViewModel.Ancestor);
                    departmentsViewModel.Init();
                    navigator.CurrentViewModel = departmentsViewModel;
                }
            }
        }
    }
}
