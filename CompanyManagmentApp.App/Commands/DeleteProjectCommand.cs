using CompanyManagmentApp.App.State.Authenticator;
using CompanyManagmentApp.App.State.Navigators;
using CompanyManagmentApp.App.ViewModels;
using CompanyManagmentApp.App.ViewModels.Factories;
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
    public class DeleteProjectCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private readonly IPermissionService permissionService;
        private readonly IAuthenticator authenticator;
        private readonly IDataService<Projects> _dataService;
        private readonly ProjectsViewModel viewModel;
        private Permission _loadedPermissions;

        public DeleteProjectCommand(IPermissionService permissionService, IAuthenticator authenticator, IDataService<Projects> dataService, ProjectsViewModel viewModel)
        {
            this.permissionService = permissionService;
            this.authenticator = authenticator;
            _dataService = dataService;
            this.viewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            Task<Permission> task = Task.Run<Permission>(async () => await permissionService.GetPermissionForModule("Project", authenticator.CurrentUser.Id));
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
            Projects project = (Projects)parameter;
            await _dataService.Delete(project.Id);
            viewModel.Init();
        }
    }
}
