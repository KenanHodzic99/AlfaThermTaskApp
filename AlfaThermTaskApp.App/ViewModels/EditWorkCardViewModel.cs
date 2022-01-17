using AlfaThermTaskApp.App.Commands;
using AlfaThermTaskApp.App.State.Authenticator;
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
    public class EditWorkCardViewModel : ViewModelBase
    {
        private string _employeeFullName;

        public string EmployeeFullName
        {
            get { return _employeeFullName; }
            set { _employeeFullName = value; OnPropertyChanged(nameof(EmployeeFullName)); }
        }

        private ObservableCollection<Projects> _projects;
        private readonly IAuthenticator authenticator;
        private readonly WorkCardViewModel workCardViewModel;
        private readonly INavigator navigator;
        private readonly IDataService<WorkCard> dataService;
        private readonly IDataService<Projects> projectsDataService;

        public ObservableCollection<Projects> Projects 
        { 
            get { return _projects; } 
            set { _projects = value; OnPropertyChanged(nameof(Projects)); } 
        }

        private Projects _selectedProjects;

        public Projects SelectedProject
        {
            get { return _selectedProjects; }
            set 
            { 
                _selectedProjects = value; 
                OnPropertyChanged(nameof(SelectedProject));
                _workCard.Project = value;
                _workCard.ProjectId = value.Id;
                OnPropertyChanged(nameof(WorkCard));
                CommandManager.InvalidateRequerySuggested();
            }
        }


        private WorkCard _workCard = new WorkCard();

        public WorkCard WorkCard
        {
            get { return _workCard; }
            set { _workCard = value; OnPropertyChanged(nameof(WorkCard)); }
        }

        public ICommand SaveWorkCard { get; }

        public EditWorkCardViewModel(IAuthenticator authenticator, WorkCardViewModel workCardViewModel, INavigator navigator, IDataService<WorkCard> dataService, IDataService<Projects> projectsDataService)
        {
            this.authenticator = authenticator;
            this.workCardViewModel = workCardViewModel;
            this.navigator = navigator;
            this.dataService = dataService;
            this.projectsDataService = projectsDataService;
            WorkCard.Employee = this.authenticator.CurrentUser.Employee;
            WorkCard.EmployeeId = this.authenticator.CurrentUser.EmployeeId;
            EmployeeFullName = this.authenticator.CurrentUser.Employee.FirstName + " " + this.authenticator.CurrentUser.Employee.LastName;
            Init();
            SaveWorkCard = new SaveWorkCardCommand(navigator,dataService,this,workCardViewModel);
        }

        public EditWorkCardViewModel(IAuthenticator authenticator, WorkCardViewModel workCardViewModel, INavigator navigator, IDataService<WorkCard> dataService, IDataService<Projects> projectsDataService, WorkCard workCard)
        {
            this.authenticator = authenticator;
            this.workCardViewModel = workCardViewModel;
            this.navigator = navigator;
            this.dataService = dataService;
            this.projectsDataService = projectsDataService;
            EmployeeFullName = workCard.Employee.FirstName + " " + workCard.Employee.LastName;
            WorkCard = workCard;
            Init();
            SaveWorkCard = new SaveWorkCardCommand(navigator, dataService, this, workCardViewModel);
        }

        public async void Init()
        {
            Projects = new ObservableCollection<Projects>(await projectsDataService.GetAll());
            OnPropertyChanged(nameof(Projects));

            if(WorkCard.Id != 0)
            {
                SelectedProject = Projects.Where(x => x.Id == WorkCard.Project.Id).FirstOrDefault();
            }
        }
    }
}
