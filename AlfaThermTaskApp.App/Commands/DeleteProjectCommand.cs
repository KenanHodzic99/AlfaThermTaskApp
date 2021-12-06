using AlfaThermTaskApp.App.State.Authenticator;
using AlfaThermTaskApp.App.State.Navigators;
using AlfaThermTaskApp.App.ViewModels;
using AlfaThermTaskApp.App.ViewModels.Factories;
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
