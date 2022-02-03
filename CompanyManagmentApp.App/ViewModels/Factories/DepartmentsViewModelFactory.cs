using CompanyManagmentApp.App.State.Authenticator;
using CompanyManagmentApp.App.State.Navigators;
using CompanyManagmentApp.DataAccess.IServices;
using CompanyManagmentApp.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyManagmentApp.App.ViewModels.Factories
{
    public class DepartmentsViewModelFactory : IViewModelFactory<DepartmentsViewModel>
    {
        private readonly IPermissionService permissionService;
        private readonly IAuthenticator authenticator;
        private readonly IDepartmentService dataService;
        private readonly INavigator navigator;

        public DepartmentsViewModelFactory(IPermissionService permissionService, IAuthenticator authenticator, IDepartmentService dataService, INavigator navigator)
        {
            this.permissionService = permissionService;
            this.authenticator = authenticator;
            this.dataService = dataService;
            this.navigator = navigator;
        }

        public DepartmentsViewModel CreateViewModel(object parameter = null)
        {
            return new DepartmentsViewModel(permissionService,authenticator,dataService,navigator);
        }
    }
}
