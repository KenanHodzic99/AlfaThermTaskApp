using AlfaThermTaskApp.App.Commands;
using AlfaThermTaskApp.App.ViewModels;
using AlfaThermTaskApp.App.ViewModels.Factories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AlfaThermTaskApp.App.State.Navigators
{
    public class Navigator : INavigator, INotifyPropertyChanged
    {
        private ViewModelBase _currentViewModel;
        public event PropertyChangedEventHandler PropertyChanged;
        public ViewModelBase CurrentViewModel
        {
            get { return _currentViewModel; }
            set 
            { 
                _currentViewModel = value;
                OnPropertyChanged(nameof(CurrentViewModel));
            }
        }

        
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
