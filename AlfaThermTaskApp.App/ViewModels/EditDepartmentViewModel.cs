using AlfaThermTaskApp.App.Commands;
using AlfaThermTaskApp.App.State.Navigators;
using AlfaThermTaskApp.DataAccess.IServices;
using AlfaThermTaskApp.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AlfaThermTaskApp.App.ViewModels
{
    public class EditDepartmentViewModel : ViewModelBase
    {
        private Departments _department = new Departments();
        private readonly IDepartmentService departmentService;
        private readonly DepartmentsViewModel departmentsViewModel;
        private readonly INavigator navigator;

        public Departments Department
        {
            get { return _department; }
            set { _department = value; OnPropertyChanged(nameof(Department)); }
        }

        private Departments _ancestor;

        public Departments Ancestor
        {
            get { return _ancestor; }
            set { _ancestor = value; OnPropertyChanged(nameof(Ancestor)); }
        }


        public ICommand SaveDepartment { get; }

        public EditDepartmentViewModel(IDepartmentService departmentService, DepartmentsViewModel departmentsViewModel, INavigator navigator)
        {
            this.departmentService = departmentService;
            this.departmentsViewModel = departmentsViewModel;
            this.navigator = navigator;
            SaveDepartment = new SaveDepartmentCommand(departmentService,departmentsViewModel,navigator,this,true,false);
        }

        public EditDepartmentViewModel(IDepartmentService departmentService, DepartmentsViewModel departmentsViewModel, INavigator navigator, Departments department, bool isEdit)
        {
            this.departmentService = departmentService;
            this.departmentsViewModel = departmentsViewModel;
            this.navigator = navigator;
            if (isEdit)
            {
                this.Department = department;
                OnPropertyChanged(nameof(Department));
                SaveDepartment = new SaveDepartmentCommand(departmentService, departmentsViewModel, navigator, this, false, true);
            }
            else
            {
                this.Ancestor = department;
                OnPropertyChanged(nameof(Ancestor));
                SaveDepartment = new SaveDepartmentCommand(departmentService, departmentsViewModel, navigator, this, false, false);
            }
        }

    }
}
