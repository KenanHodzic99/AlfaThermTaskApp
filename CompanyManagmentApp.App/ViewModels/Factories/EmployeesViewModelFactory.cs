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
    public class EmployeesViewModelFactory : IViewModelFactory<EmployeesViewModel>
    {
        private readonly IDataService<Employees> dataService;
        private readonly INavigator navigator;
        private readonly IAuthenticator authenticator;
        private readonly IPermissionService permissionService;
        private readonly IDataService<Jobs> jobsDataService;

        public EmployeesViewModelFactory(IDataService<Employees> dataService, INavigator navigator, 
            IAuthenticator authenticator, IPermissionService permissionService, IDataService<Jobs> jobsDataService)
        {
            this.dataService = dataService;
            this.navigator = navigator;
            this.authenticator = authenticator;
            this.permissionService = permissionService;
            this.jobsDataService = jobsDataService;
        }

        public EmployeesViewModel CreateViewModel(object parameter = null)
        {
            return new EmployeesViewModel(dataService,navigator,authenticator,permissionService,jobsDataService);
        }
    }
}
