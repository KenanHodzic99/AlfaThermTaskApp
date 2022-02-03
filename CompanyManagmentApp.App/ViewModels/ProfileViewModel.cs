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
    public class ProfileViewModel : ViewModelBase
    {
        private Users _currentUser;
        public Users CurrentUser
        {
            get { return _currentUser; }
            set { _currentUser = value; OnPropertyChanged(nameof(CurrentUser)); }
        }

        private Employees _currentEmployee;
        public Employees CurrentEmployee
        {
            get { return _currentEmployee; }
            set { _currentEmployee = value; OnPropertyChanged(nameof(CurrentEmployee)); }
        }

        private readonly IAuthenticator authenticator;
        private readonly IDataService<Users> dataService;
        private readonly INavigator navigator;

        public ICommand ChangePassword { get; }
        public ICommand SaveChanges { get; }

        public ProfileViewModel(IAuthenticator authenticator, IDataService<Users> dataService, INavigator navigator)
        {
            this.authenticator = authenticator;
            this.dataService = dataService;
            this.navigator = navigator;
            Init();
            ChangePassword = new ChangePasswordCommand(navigator,this,authenticator,dataService);
            SaveChanges = new SaveEmployeeChangesCommand(this, dataService, this.authenticator);
        }

        public async void Init()
        {
            CurrentUser = await dataService.Get(authenticator.CurrentUser.Id);
            CurrentEmployee = CurrentUser.Employee;
        }
    }
}
