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
    public class SaveJobCommand : ICommand
    {
        private readonly IDataService<Jobs> dataService;
        private readonly INavigator navigator;
        private readonly EditJobViewModel editJobViewModel;
        private readonly JobsViewModel jobViewModel;

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

        public SaveJobCommand(IDataService<Jobs> dataService, INavigator navigator, EditJobViewModel editJobViewModel, JobsViewModel jobViewModel)
        {
            this.dataService = dataService;
            this.navigator = navigator;
            this.editJobViewModel = editJobViewModel;
            this.jobViewModel = jobViewModel;
        }

        public bool CanExecute(object parameter)
        {
            var newJob = editJobViewModel.Job;
            if(string.IsNullOrWhiteSpace(newJob.JobTitle) && newJob.Department == null)
            {
                return false;
            }
            else
            {
                return true;
            }
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
