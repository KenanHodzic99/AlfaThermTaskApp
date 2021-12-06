using AlfaThermTaskApp.App.State.Authenticator;
using AlfaThermTaskApp.App.State.Navigators;
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
    public class SavePasswordCommand : ICommand
    {
        private readonly IAuthenticator authenticator;
        private readonly ChangePasswordViewModel changePasswordViewModel;
        private readonly INavigator Navigator;
        private readonly ProfileViewModel ProfileViewModel;
        private readonly IDataService<Users> DataService;

        public event EventHandler CanExecuteChanged;


        public SavePasswordCommand(IAuthenticator authenticator, INavigator navigator, ProfileViewModel profileViewModel, IDataService<Users> dataService, ChangePasswordViewModel changePasswordViewModel)
        {
            this.authenticator = authenticator;
            Navigator = navigator;
            ProfileViewModel = profileViewModel;
            DataService = dataService;
            this.changePasswordViewModel = changePasswordViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public async void Execute(object parameter)
        {
            Users currentUser = authenticator.CurrentUser;
            if(await authenticator.Login(currentUser.Username, changePasswordViewModel.OldPass))
            {
                await authenticator.ChangePassword(currentUser, changePasswordViewModel.NewPass);
                Navigator.CurrentViewModel = ProfileViewModel;
            }
            else
            {

            }
        }
    }
}
