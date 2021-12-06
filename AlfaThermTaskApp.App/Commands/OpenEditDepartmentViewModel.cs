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
    public class OpenEditDepartmentViewModel : ICommand
    {
        private readonly IPermissionService permissionService;
        private readonly IAuthenticator authenticator;
        private readonly INavigator navigator;
        private readonly IDepartmentService departmentService;
        private readonly DepartmentsViewModel departmentViewModel;
        private readonly bool isEdit;
        private Permission _loadedPermissions;

        public event EventHandler CanExecuteChanged;

        public OpenEditDepartmentViewModel(IPermissionService permissionService, IAuthenticator authenticator, INavigator navigator, IDepartmentService departmentService, DepartmentsViewModel departmentViewModel, bool isEdit)
        {
            this.permissionService = permissionService;
            this.authenticator = authenticator;
            this.navigator = navigator;
            this.departmentService = departmentService;
            this.departmentViewModel = departmentViewModel;
            this.isEdit = isEdit;
        }

        public bool CanExecute(object parameter)
        {
            Task<Permission> task = Task.Run<Permission>(async () => await permissionService.GetPermissionForModule("Department", authenticator.CurrentUser.Id));
            _loadedPermissions = task.Result;
            if (isEdit)
            {
                if ((bool)_loadedPermissions.CanEdit)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                if ((bool)_loadedPermissions.CanWrite)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public void Execute(object parameter)
        {
            if(parameter != null)
            {
                if (isEdit)
                {
                    Departments newDepartment = new Departments();
                    var paramDepartment = (DepartmentsGroup)parameter;
                    newDepartment.Id = paramDepartment.Key;
                    newDepartment.DepartmentName = paramDepartment.Name;
                    navigator.CurrentViewModel = new EditDepartmentViewModel(departmentService, departmentViewModel, navigator, newDepartment, isEdit);
                }
                else
                {
                    Departments ancestor = new Departments();
                    var paramDepartment = (DepartmentsGroup)parameter;
                    ancestor.Id = paramDepartment.Key;
                    ancestor.DepartmentName = paramDepartment.Name;
                    navigator.CurrentViewModel = new EditDepartmentViewModel(departmentService, departmentViewModel, navigator, ancestor, isEdit);
                }
            }
            else
            {
                navigator.CurrentViewModel = new EditDepartmentViewModel(departmentService,departmentViewModel,navigator);
            }
        }
    }
}
