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
    public class WorkCardViewModel : ViewModelBase
    {
        private ObservableCollection<WorkCard> _workCards;
        private readonly IPermissionService permissionService;
        private readonly INavigator navigator;
        private readonly IDataService<WorkCard> dataService;
        private readonly IAuthenticator authenticator;
        private readonly IDataService<Projects> projectsDataService;

        public ObservableCollection<WorkCard> WorkCards
        {
            get { return _workCards; }
            set 
            { 
                _workCards = value;
                OnPropertyChanged(nameof(WorkCards));
            }
        }

        public ICommand AddWorkCard { get; }
        public ICommand EditWorkCard { get; }
        public ICommand DeleteWorkCard { get; }

        public WorkCardViewModel(IPermissionService permissionService, INavigator navigator, IDataService<WorkCard> dataService, IAuthenticator authenticator, IDataService<Projects> projectsDataService)
        {
            this.permissionService = permissionService;
            this.navigator = navigator;
            this.dataService = dataService;
            this.authenticator = authenticator;
            this.projectsDataService = projectsDataService;
            Init();
            DeleteWorkCard = new DeleteWorkCardCommand(this.permissionService,this.authenticator,this.dataService,this);
            AddWorkCard = new OpenEditWorkCardViewModel(this.permissionService,this.navigator, this.authenticator,this, this.dataService, this.projectsDataService, false);
            EditWorkCard = new OpenEditWorkCardViewModel(this.permissionService, this.navigator, this.authenticator, this, this.dataService, this.projectsDataService, true);
        }

        public async void Init()
        {
            WorkCards = new ObservableCollection<WorkCard>(await dataService.GetAll(authenticator.CurrentUser.EmployeeId));
            OnPropertyChanged(nameof(WorkCards));
        }

    }
}
