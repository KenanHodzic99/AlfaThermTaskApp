using AlfaThermTaskApp.App.State.Authenticator;
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
    public class DeleteJobCommand : ICommand
    {
        private readonly IPermissionService permissionService;
        private readonly IAuthenticator authenticator;
        private readonly IDataService<Jobs> dataService;
        private readonly JobsViewModel jobsViewModel;
        private Permission _loadedPermissions;

        public event EventHandler CanExecuteChanged;

        public DeleteJobCommand(IPermissionService permissionService, IAuthenticator authenticator, IDataService<Jobs> dataService, JobsViewModel jobsViewModel)
        {
            this.permissionService = permissionService;
            this.authenticator = authenticator;
            this.dataService = dataService;
            this.jobsViewModel = jobsViewModel;
        }

        public bool CanExecute(object parameter)
        {
            Task<Permission> task = Task.Run<Permission>(async () => await permissionService.GetPermissionForModule("Jobs", authenticator.CurrentUser.Id));
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
            Jobs job = (Jobs)parameter;
            await dataService.Delete(job.Id);
            jobsViewModel.Init();
        }
    }
}
