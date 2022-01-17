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
    public class SaveProjectCommand : ICommand
    {
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

        private readonly EditProjectViewModel _viewModel;
        private readonly IDataService<Projects> _dataService;
        private readonly ProjectsViewModel projectsViewModel;
        private readonly INavigator navigator;

        public SaveProjectCommand(EditProjectViewModel viewModel, IDataService<Projects> dataService, ProjectsViewModel projectsViewModel, INavigator navigator)
        {
            _viewModel = viewModel;
            _dataService = dataService;
            this.projectsViewModel = projectsViewModel;
            this.navigator = navigator;
        }

        public bool CanExecute(object parameter)
        {
            var newProject = _viewModel.Project;
            if(string.IsNullOrWhiteSpace(newProject.ProjectLocation) || string.IsNullOrWhiteSpace(newProject.ProjectName)
                || string.IsNullOrWhiteSpace(newProject.ProjectStatus) || string.IsNullOrWhiteSpace(newProject.ProjectType))
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
            var newProject = _viewModel.Project;
            if(newProject.Id != 0)
            {
                await _dataService.Update(newProject.Id, newProject);
                projectsViewModel.Init();
                navigator.CurrentViewModel = projectsViewModel;
            }
            else
            {
                await _dataService.Create(newProject);
                projectsViewModel.Init();
                navigator.CurrentViewModel = projectsViewModel;
            }
        }
    }
}
