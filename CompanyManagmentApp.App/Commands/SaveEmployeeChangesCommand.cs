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
    public class SaveEmployeeChangesCommand : ICommand
    {
        private readonly ProfileViewModel profileViewModel;
        private readonly IDataService<Users> dataService;
        private readonly IAuthenticator authenticator;

        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }

        public SaveEmployeeChangesCommand(ProfileViewModel profileViewModel, IDataService<Users> dataService, IAuthenticator authenticator)
        {
            this.profileViewModel = profileViewModel;
            this.dataService = dataService;
            this.authenticator = authenticator;
        }


        public bool CanExecute(object parameter)
        {
            var Employee = this.authenticator.CurrentUser.Employee;
            if(Employee.DateOfBirth.HasValue && !string.IsNullOrWhiteSpace(Employee.PersonalIdentityNumber)
                && !string.IsNullOrWhiteSpace(Employee.Adress) && !string.IsNullOrWhiteSpace(Employee.Email) && !string.IsNullOrWhiteSpace(Employee.PhoneNumber))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async void Execute(object parameter)
        {
            Users currentUser = profileViewModel.CurrentUser;
            await dataService.Update(currentUser.Id, currentUser);
            profileViewModel.Init();
        }
    }
}
