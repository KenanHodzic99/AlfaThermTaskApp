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
    public class ProfileViewModelFactory : IViewModelFactory<ProfileViewModel>
    {
        private readonly IAuthenticator authenticator;
        private readonly IDataService<Users> dataService;
        private readonly INavigator navigator;

        public ProfileViewModelFactory(IAuthenticator authenticator, IDataService<Users> dataService, INavigator navigator)
        {
            this.authenticator = authenticator;
            this.dataService = dataService;
            this.navigator = navigator;
        }
        public ProfileViewModel CreateViewModel(object parameter = null)
        {
            return new ProfileViewModel(authenticator,dataService,navigator);
        }
    }
}
