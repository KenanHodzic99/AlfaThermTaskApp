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
    public class SaveJobCommand : ICommand
    {
        private readonly IDataService<Jobs> dataService;
        private readonly INavigator navigator;
        private readonly EditJobViewModel editJobViewModel;
        private readonly JobsViewModel jobViewModel;

        public event EventHandler CanExecuteChanged;

        public SaveJobCommand(IDataService<Jobs> dataService, INavigator navigator, EditJobViewModel editJobViewModel, JobsViewModel jobViewModel)
        {
            this.dataService = dataService;
            this.navigator = navigator;
            this.editJobViewModel = editJobViewModel;
            this.jobViewModel = jobViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public async void Execute(object parameter)
        {
            var newJob = editJobViewModel.Job;
            if(newJob.Id != 0)
            {
                await dataService.Update(newJob.Id, newJob);
                jobViewModel.Init();
                navigator.CurrentViewModel = jobViewModel;
            }
            else
            {
                await dataService.Create(newJob);
                jobViewModel.Init();
                navigator.CurrentViewModel = jobViewModel;
            }
        }
    }
}
