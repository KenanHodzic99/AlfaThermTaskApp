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
    public class JobsViewModelFactory : IViewModelFactory<JobsViewModel>
    {
        private readonly IPermissionService permissionService;
        private readonly IAuthenticator authenticator;
        private readonly IDepartmentService departmentService;
        private readonly IDataService<Jobs> dataService;
        private readonly INavigator navigator;

        public JobsViewModelFactory(IPermissionService permissionService, IAuthenticator authenticator, IDepartmentService departmentService, IDataService<Jobs> dataService, INavigator navigator)
        {
            this.permissionService = permissionService;
            this.authenticator = authenticator;
            this.departmentService = departmentService;
            this.dataService = dataService;
            this.navigator = navigator;
        }

        public JobsViewModel CreateViewModel(object parameter = null)
        {
            return new JobsViewModel(permissionService, authenticator, departmentService, dataService, navigator);
        }
    }
}
