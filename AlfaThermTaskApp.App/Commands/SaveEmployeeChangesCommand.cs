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
