using AlfaThermTaskApp.App.State.Authenticator;
using AlfaThermTaskApp.App.State.Navigators;
using AlfaThermTaskApp.DataAccess.IServices;
using AlfaThermTaskApp.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlfaThermTaskApp.App.ViewModels.Factories
{
    public class ProjectViewModelFactory : IViewModelFactory<ProjectsViewModel>
    {
        IDataService<Projects> dataService;
        private readonly IPermissionService permissionService;
        private readonly IAuthenticator authenticator;
        INavigator _navigator;

        public ProjectViewModelFactory(IPermissionService permissionService, IAuthenticator authenticator, INavigator navigator, IDataService<Projects> dataService)
        {
            this.dataService = dataService;
            this.permissionService = permissionService;
            this.authenticator = authenticator;
            _navigator = navigator;
        }

        public ProjectsViewModel CreateViewModel(object parameter = null)
        {
            return new ProjectsViewModel(permissionService, authenticator, _navigator ,dataService);
        }
    }
}
