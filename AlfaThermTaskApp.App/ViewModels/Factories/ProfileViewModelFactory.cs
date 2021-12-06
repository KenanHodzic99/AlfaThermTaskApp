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
