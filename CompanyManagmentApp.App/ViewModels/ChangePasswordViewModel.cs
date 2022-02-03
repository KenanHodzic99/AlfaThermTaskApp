using CompanyManagmentApp.App.Commands;
using CompanyManagmentApp.App.State.Authenticator;
using CompanyManagmentApp.App.State.Navigators;
using CompanyManagmentApp.DataAccess.IServices;
using CompanyManagmentApp.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CompanyManagmentApp.App.ViewModels
{
    public class ChangePasswordViewModel : ViewModelBase
    {
        public ICommand SavePassword { get; }

        private string _oldPass;

        public string OldPass
        {
            get { return _oldPass; }
            set { _oldPass = value; OnPropertyChanged(nameof(OldPass)); }
        }

        private string _newPass;

        public string NewPass
        {
            get { return _newPass; }
            set { _newPass = value; OnPropertyChanged(nameof(NewPass)); }
        }

        private string _reNewPass;
        
        public string ReNewPass
        {
            get { return _reNewPass; }
            set { _reNewPass = value; OnPropertyChanged(nameof(ReNewPass)); }
        }

        private readonly ProfileViewModel profileViewModel;
        private readonly IAuthenticator authenticator;
        private readonly IDataService<Users> dataService;
        private readonly INavigator navigator;

        public ChangePasswordViewModel(ProfileViewModel profileViewModel, IAuthenticator authenticator, IDataService<Users> dataService, INavigator navigator)
        {
            this.profileViewModel = profileViewModel;
            this.authenticator = authenticator;
            this.dataService = dataService;
            this.navigator = navigator;
            SavePassword = new SavePasswordCommand(authenticator,navigator,profileViewModel,dataService,this);
        }
    }
}
