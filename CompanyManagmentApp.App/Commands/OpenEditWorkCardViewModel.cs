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
    public class OpenEditWorkCardViewModel : ICommand
    {
        private readonly IPermissionService permissionService;
        private readonly INavigator navigator;
        private readonly IAuthenticator authenticator;
        private readonly WorkCardViewModel workCardViewModel;
        private readonly IDataService<WorkCard> dataService;
        private readonly IDataService<Projects> projectsDataService;
        private readonly bool isEdit;
        private Permission _loadedPermissions;

        public event EventHandler CanExecuteChanged;

        public OpenEditWorkCardViewModel(IPermissionService permissionService, INavigator navigator,IAuthenticator authenticator, WorkCardViewModel workCardViewModel, IDataService<WorkCard> dataService, IDataService<Projects> projectsDataService, bool isEdit)
        {
            this.permissionService = permissionService;
            this.navigator = navigator;
            this.authenticator = authenticator;
            this.workCardViewModel = workCardViewModel;
            this.dataService = dataService;
            this.projectsDataService = projectsDataService;
            this.isEdit = isEdit;
        }

        public bool CanExecute(object parameter)
        {
            Task<Permission> task = Task.Run<Permission>(async () => await permissionService.GetPermissionForModule("Work card", authenticator.CurrentUser.Id));
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
            if (parameter != null)
            {
                WorkCard newWorkCard = (WorkCard)parameter;
                navigator.CurrentViewModel = new EditWorkCardViewModel(authenticator, workCardViewModel, navigator, dataService, projectsDataService, newWorkCard);
            }
            else
            {
                navigator.CurrentViewModel = new EditWorkCardViewModel(authenticator, workCardViewModel, navigator, dataService, projectsDataService);
            }
        }
    }
}
