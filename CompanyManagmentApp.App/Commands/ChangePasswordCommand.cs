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
    public class ChangePasswordCommand : ICommand
    {
        private readonly INavigator navigator;
        private readonly ProfileViewModel profileViewModel;
        private readonly IAuthenticator authenticator;
        private readonly IDataService<Users> dataService;

        public event EventHandler CanExecuteChanged;

        public ChangePasswordCommand(INavigator navigator, ProfileViewModel profileViewModel, IAuthenticator authenticator, IDataService<Users> dataService)
        {
            this.navigator = navigator;
            this.profileViewModel = profileViewModel;
            this.authenticator = authenticator;
            this.dataService = dataService;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            navigator.CurrentViewModel = new ChangePasswordViewModel(profileViewModel,authenticator,dataService,navigator);
        }
    }
}
