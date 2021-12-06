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
    public class OpenEditProjectViewModel : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private readonly IPermissionService permissionService;
        private readonly IAuthenticator authenticator;
        private readonly INavigator _navigator;
        private readonly IDataService<Projects> _dataService;
        private readonly ProjectsViewModel viewModel;
        private readonly bool isEdit;
        private Permission _loadedPermissions;

        public OpenEditProjectViewModel(IPermissionService permissionService, IAuthenticator authenticator, INavigator navigator, IDataService<Projects> dataService, ProjectsViewModel viewModel, bool isEdit)
        {
            this.permissionService = permissionService;
            this.authenticator = authenticator;
            _navigator = navigator;
            _dataService = dataService;
            this.viewModel = viewModel;
            this.isEdit = isEdit;
        }

        public bool CanExecute(object parameter)
        {
            Task<Permission> task = Task.Run<Permission>(async () => await permissionService.GetPermissionForModule("Project", authenticator.CurrentUser.Id));
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
                _navigator.CurrentViewModel = new EditProjectViewModel(_navigator, viewModel, _dataService, (Projects)parameter);
            else
                _navigator.CurrentViewModel = new EditProjectViewModel(_navigator, viewModel, _dataService);
        }
    }
}
