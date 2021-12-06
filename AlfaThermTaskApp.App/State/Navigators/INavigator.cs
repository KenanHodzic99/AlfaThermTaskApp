using AlfaThermTaskApp.App.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AlfaThermTaskApp.App.State.Navigators
{
    public enum ViewType
    {
        Login,
        WorkCard,
        Projects,
        Profile,
        Jobs,
        Departments,
        Employees
    }
    public interface INavigator
    {
        ViewModelBase CurrentViewModel { get; set; }
    }
}
