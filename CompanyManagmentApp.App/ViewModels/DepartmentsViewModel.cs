using CompanyManagmentApp.App.Commands;
using CompanyManagmentApp.App.State.Authenticator;
using CompanyManagmentApp.App.State.Navigators;
using CompanyManagmentApp.DataAccess.IServices;
using CompanyManagmentApp.DatabaseModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CompanyManagmentApp.App.ViewModels
{
    public class DepartmentsGroup
    {
        public HierarchyId Key { get; set; }
        public string Name { get; set; }

        public ObservableCollection<DepartmentsGroup> SubGroups { get; set; }
        public ObservableCollection<object> Items
        {
            get
            {
                ObservableCollection<object> childNodes = new ObservableCollection<object>();
                if (this.SubGroups != null)
                {
                    foreach (var group in this.SubGroups)
                        childNodes.Add(group);
                }
                return childNodes;
            }
        }
    }
    public class DepartmentsViewModel : ViewModelBase
    {
        private ObservableCollection<Departments> _departments = new ObservableCollection<Departments>();
        private readonly IPermissionService permissionService;
        private readonly IAuthenticator authenticator;
        private readonly IDepartmentService departmentService;
        private readonly INavigator navigator;

        public ObservableCollection<Departments> Departments
        {
            get { return _departments; }
            set { _departments = value; OnPropertyChanged(nameof(Departments)); }
        }

        private ObservableCollection<DepartmentsGroup> _departmentsGroup = new ObservableCollection<DepartmentsGroup>();

        public ObservableCollection<DepartmentsGroup> DepartmentsGroup
        {
            get { return _departmentsGroup; }
            set { _departmentsGroup = value; OnPropertyChanged(nameof(DepartmentsGroup)); }
        }

        public ICommand AddSubDepartment { get; }
        public ICommand AddRootDepartment { get; }
        public ICommand EditDepartment { get; }
        public ICommand DeleteDepartment { get; }

        public DepartmentsViewModel(IPermissionService permissionService, IAuthenticator authenticator, IDepartmentService departmentService, INavigator navigator)
        {
            this.permissionService = permissionService;
            this.authenticator = authenticator;
            this.departmentService = departmentService;
            this.navigator = navigator;
            Init();
            AddSubDepartment = new OpenEditDepartmentViewModel(this.permissionService, this.authenticator, this.navigator, this.departmentService, this, false);
            AddRootDepartment = new OpenEditDepartmentViewModel(this.permissionService, this.authenticator, this.navigator, this.departmentService, this, false);
            EditDepartment = new OpenEditDepartmentViewModel(this.permissionService, this.authenticator, this.navigator, this.departmentService, this, true);
            DeleteDepartment = new DeleteDepartmentCommand(this.permissionService, this.authenticator, this.departmentService, this);
        }

        public async void Init()
        {
            Departments.Clear();
            DepartmentsGroup.Clear();

            Departments = new ObservableCollection<Departments>(await departmentService.GetAll());
            
            foreach (var department in Departments)
            {
                if(department.Id.GetLevel() == 1) 
                {
                    DepartmentsGroup.Add(new DepartmentsGroup() { Key = department.Id, Name = department.DepartmentName });
                }
            }

            MakeTree(DepartmentsGroup, Departments);

            OnPropertyChanged(nameof(this.DepartmentsGroup));
            OnPropertyChanged(nameof(this.Departments));
        }

        public void MakeTree(ObservableCollection<DepartmentsGroup> DepartmentsGroup, ObservableCollection<Departments> Departments)
        {
            foreach (var departmentGroup in DepartmentsGroup)
            {
                foreach (var department in Departments)
                {
                    if (department.Id.IsDescendantOf(departmentGroup.Key) && department.Id != departmentGroup.Key)
                    {
                        if (departmentGroup.SubGroups == null)
                        {
                            departmentGroup.SubGroups = new ObservableCollection<DepartmentsGroup>();
                        }
                        if (department.Id.GetAncestor(1) == departmentGroup.Key)
                        {
                            departmentGroup.SubGroups.Add(new DepartmentsGroup() { Key = department.Id, Name = department.DepartmentName });
                        }
                        else
                        {
                            MakeTree(departmentGroup.SubGroups, Departments);
                        }
                    }

                }
            }
        }
    }
}
