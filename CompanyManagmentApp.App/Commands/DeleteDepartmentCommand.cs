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
    public class DeleteDepartmentCommand : ICommand
    {
        private readonly IPermissionService permissionService;
        private readonly IAuthenticator authenticator;
        private readonly IDepartmentService departmentService;
        private readonly DepartmentsViewModel departmentViewModel;
        private Permission _loadedPermissions;

        public event EventHandler CanExecuteChanged;

        public DeleteDepartmentCommand(IPermissionService permissionService, IAuthenticator authenticator, IDepartmentService departmentService, DepartmentsViewModel departmentViewModel)
        {
            this.permissionService = permissionService;
            this.authenticator = authenticator;
            this.departmentService = departmentService;
            this.departmentViewModel = departmentViewModel;
        }

        public bool CanExecute(object parameter)
        {
            Task<Permission> task = Task.Run<Permission>(async () => await permissionService.GetPermissionForModule("Department", authenticator.CurrentUser.Id));
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
            DepartmentsGroup department = (DepartmentsGroup)parameter;
            await departmentService.Delete(department.Key);
            departmentViewModel.Init();
        }
    }
}
