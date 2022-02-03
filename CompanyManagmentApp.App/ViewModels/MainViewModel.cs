using CompanyManagmentApp.App.Commands;
using CompanyManagmentApp.App.State.Authenticator;
using CompanyManagmentApp.App.State.Navigators;
using CompanyManagmentApp.App.ViewModels.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CompanyManagmentApp.App.ViewModels
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
