using AlfaThermTaskApp.App.State.Navigators;
using AlfaThermTaskApp.App.ViewModels;
using AlfaThermTaskApp.App.ViewModels.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AlfaThermTaskApp.App.Commands
{
    public class UpdateCurrentViewModel : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private readonly INavigator _navigator;
        private readonly IViewModelAbstractFactory _viewModelFactory;

        public UpdateCurrentViewModel(INavigator navigator, IViewModelAbstractFactory viewModelFactory)
        {
            _navigator = navigator;
            _viewModelFactory = viewModelFactory;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if(parameter is ViewType)
            {
                ViewType viewType = (ViewType)parameter;

                _navigator.CurrentViewModel = _viewModelFactory.CreateViewModel(viewType);
            }
        }
    }
}
