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
    public class DeleteWorkCardCommand : ICommand
    {
        private readonly IPermissionService permissionService;
        private readonly IAuthenticator authenticator;
        private readonly IDataService<WorkCard> dataService;
        private readonly WorkCardViewModel viewModel;
        private Permission _loadedPermissions;

        public event EventHandler CanExecuteChanged;

        public DeleteWorkCardCommand(IPermissionService permissionService, IAuthenticator authenticator, IDataService<WorkCard> dataService, WorkCardViewModel viewModel)
        {
            this.permissionService = permissionService;
            this.authenticator = authenticator;
            this.dataService = dataService;
            this.viewModel = viewModel;
            
        }


        public bool CanExecute(object parameter)
        {
            Task<Permission> task = Task.Run<Permission>(async () => await permissionService.GetPermissionForModule("Work card", authenticator.CurrentUser.Id));
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
            WorkCard workCard = (WorkCard)parameter;
            await dataService.Delete(workCard.Id);
            viewModel.Init();
        }
    }
}
