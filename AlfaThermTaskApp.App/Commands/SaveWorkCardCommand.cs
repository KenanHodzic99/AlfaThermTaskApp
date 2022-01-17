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
    public class SaveWorkCardCommand : ICommand
    {
        private readonly INavigator navigator;
        private readonly IDataService<WorkCard> dataService;
        private readonly EditWorkCardViewModel editWorkCardViewModel;
        private readonly WorkCardViewModel workCardViewModel;

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

        public SaveWorkCardCommand(INavigator navigator, IDataService<WorkCard> dataService, EditWorkCardViewModel editWorkCardViewModel, WorkCardViewModel workCardViewModel)
        {
            this.navigator = navigator;
            this.dataService = dataService;
            this.editWorkCardViewModel = editWorkCardViewModel;
            this.workCardViewModel = workCardViewModel;
        }

        public bool CanExecute(object parameter)
        {
            WorkCard newWorkCard = editWorkCardViewModel.WorkCard;

            if(!string.IsNullOrWhiteSpace(newWorkCard.Activities) && newWorkCard.NumberOfWorkHours != 0 && newWorkCard.Project != null)
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
