using AlfaThermTaskApp.App.Commands;
using AlfaThermTaskApp.App.State.Navigators;
using AlfaThermTaskApp.DataAccess.IServices;
using AlfaThermTaskApp.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AlfaThermTaskApp.App.ViewModels
{
    public class EditJobViewModel : ViewModelBase
    {
        private Jobs _job = new Jobs();

        public Jobs Job
        {
            get { return _job; }
            set { _job = value; OnPropertyChanged(nameof(Job)); }
        }

        private ObservableCollection<Departments> _departments;

        public ObservableCollection<Departments> Departments
        {
            get { return _departments; }
            set { _departments = value; OnPropertyChanged(nameof(Departments)); }
        }

        private Departments _selectedDepartment;
        private readonly INavigator navigator;
        private readonly IDataService<Jobs> dataService;
        private readonly IDepartmentService departmentService;
        private readonly JobsViewModel jobsViewModel;

        public Departments SelectedDepartment
        {
            get { return _selectedDepartment; }
            set 
            { 
                _selectedDepartment = value; 
                OnPropertyChanged(nameof(SelectedDepartment));
                Job.Department = value;
                Job.DepartmentId = value.Id;
                OnPropertyChanged(nameof(Job));
            }
        }

        public ICommand SaveJob { get; }

        public EditJobViewModel(INavigator navigator, IDataService<Jobs> dataService, IDepartmentService departmentService, JobsViewModel jobsViewModel)
        {
            this.navigator = navigator;
            this.dataService = dataService;
            this.departmentService = departmentService;
            this.jobsViewModel = jobsViewModel;
            Init();
            SaveJob = new SaveJobCommand(dataService, navigator, this, jobsViewModel);
        }

        public EditJobViewModel(INavigator navigator, IDataService<Jobs> dataService, IDepartmentService departmentService, JobsViewModel jobsViewModel, Jobs newJob)
        {
            this.navigator = navigator;
            this.dataService = dataService;
            this.departmentService = departmentService;
            this.jobsViewModel = jobsViewModel;
            Job = newJob;
            OnPropertyChanged(nameof(Job));
            Init();
            OnPropertyChanged(nameof(SelectedDepartment));
            SaveJob = new SaveJobCommand(dataService,navigator,this,jobsViewModel);
        }

        public async void Init()
        {
            Departments = new ObservableCollection<Departments>(await departmentService.GetAll());
            OnPropertyChanged(nameof(Departments));

            if (Job.Id != 0)
            {
                SelectedDepartment = Departments.Where(x => x.Id == Job.DepartmentId).FirstOrDefault();
            }
        }

    }
}
