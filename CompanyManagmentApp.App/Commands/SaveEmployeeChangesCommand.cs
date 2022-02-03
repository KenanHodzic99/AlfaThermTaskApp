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

        public event EventHandler CanExecuteChanged;

        public SaveEmployeeChangesCommand(ProfileViewModel profileViewModel, IDataService<Users> dataService)
        {
            this.profileViewModel = profileViewModel;
            this.dataService = dataService;
        }


        public bool CanExecute(object parameter)
        {
            return true;
        }

        public async void Execute(object parameter)
        {
            Users currentUser = profileViewModel.CurrentUser;
            await dataService.Update(currentUser.Id, currentUser);
            profileViewModel.Init();
        }
    }
}
