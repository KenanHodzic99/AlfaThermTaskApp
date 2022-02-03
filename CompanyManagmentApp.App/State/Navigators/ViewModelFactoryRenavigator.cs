using CompanyManagmentApp.App.ViewModels;
using CompanyManagmentApp.App.ViewModels.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyManagmentApp.App.State.Navigators
{
    public class ViewModelFactoryRenavigator<T> : IRenavigator where T : ViewModelBase
    {
        private readonly INavigator navigator;
        public IViewModelFactory<T> ViewModelFactory { get; }

        public ViewModelFactoryRenavigator(INavigator navigator, IViewModelFactory<T> viewModelFactory)
        {
            this.navigator = navigator;
            ViewModelFactory = viewModelFactory;
        }

        

        public void Renavigate()
        {
            navigator.CurrentViewModel = ViewModelFactory.CreateViewModel();
        }
    }
}
