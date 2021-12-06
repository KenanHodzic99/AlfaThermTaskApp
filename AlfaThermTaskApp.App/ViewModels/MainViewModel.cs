using AlfaThermTaskApp.App.Commands;
using AlfaThermTaskApp.App.State.Authenticator;
using AlfaThermTaskApp.App.State.Navigators;
using AlfaThermTaskApp.App.ViewModels.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AlfaThermTaskApp.App.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IViewModelAbstractFactory viewModelFactory;
        public INavigator Navigator { get; set; }
        public ICommand UpdateCurrentViewModelCommand { get; }
        public IAuthenticator Authenticator { get; }
        public MainViewModel(INavigator navigator, IViewModelAbstractFactory viewModelFactory, IAuthenticator authenticator)
        {
            Navigator = navigator;
            this.viewModelFactory = viewModelFactory;
            Authenticator = authenticator;
            UpdateCurrentViewModelCommand = new UpdateCurrentViewModel(navigator, viewModelFactory);
            UpdateCurrentViewModelCommand.Execute(ViewType.Login);
        }
    }
}
