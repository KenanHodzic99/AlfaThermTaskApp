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
    public class SaveWorkCardCommand : ICommand
    {
        private readonly INavigator navigator;
        private readonly IDataService<WorkCard> dataService;
        private readonly EditWorkCardViewModel editWorkCardViewModel;
        private readonly WorkCardViewModel workCardViewModel;

        public event EventHandler CanExecuteChanged;

        public SaveWorkCardCommand(INavigator navigator, IDataService<WorkCard> dataService, EditWorkCardViewModel editWorkCardViewModel, WorkCardViewModel workCardViewModel)
        {
            this.navigator = navigator;
            this.dataService = dataService;
            this.editWorkCardViewModel = editWorkCardViewModel;
            this.workCardViewModel = workCardViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public async void Execute(object parameter)
        {
            WorkCard newWorkCard = editWorkCardViewModel.WorkCard;
            if(newWorkCard.Id != 0)
            {
                await dataService.Update(newWorkCard.Id, newWorkCard);
                workCardViewModel.Init();
                navigator.CurrentViewModel = workCardViewModel;
            }
            else
            {
                await dataService.Create(newWorkCard);
                workCardViewModel.Init();
                navigator.CurrentViewModel = workCardViewModel;
            }
        }
    }
}
