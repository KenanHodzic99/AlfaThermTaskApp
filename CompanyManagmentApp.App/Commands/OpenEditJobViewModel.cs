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
    public class OpenEditJobViewModel : ICommand
    {
        private readonly IPermissionService permissionService;
        private readonly IAuthenticator authenticator;
        private readonly INavigator navigator;
        private readonly IDataService<Jobs> dataService;
        private readonly IDepartmentService departmentService;
        private readonly JobsViewModel jobsViewModel;
        private readonly bool isEdit;
        private Permission _loadedPermissions;

        public event EventHandler CanExecuteChanged;

        public OpenEditJobViewModel(IPermissionService permissionService, IAuthenticator authenticator, INavigator navigator, IDataService<Jobs> dataService, IDepartmentService departmentService, JobsViewModel jobsViewModel, bool isEdit)
        {
            this.permissionService = permissionService;
            this.authenticator = authenticator;
            this.navigator = navigator;
            this.dataService = dataService;
            this.departmentService = departmentService;
            this.jobsViewModel = jobsViewModel;
            this.isEdit = isEdit;
        }

        public bool CanExecute(object parameter)
        {
            Task<Permission> task = Task.Run<Permission>(async () => await permissionService.GetPermissionForModule("Jobs", authenticator.CurrentUser.Id));
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
                };
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
                };
            }
        }

        public void Execute(object parameter)
        {
            if(parameter != null) 
            {
                navigator.CurrentViewModel = new EditJobViewModel(navigator, dataService, departmentService, jobsViewModel, (Jobs)parameter);
            }
            else 
            {
               navigator.CurrentViewModel = new EditJobViewModel(navigator,dataService,departmentService,jobsViewModel);
            }
        }
    }
}
