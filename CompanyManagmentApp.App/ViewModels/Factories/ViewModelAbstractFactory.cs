using CompanyManagmentApp.App.State.Authenticator;
using CompanyManagmentApp.App.State.Navigators;
using CompanyManagmentApp.DataAccess.IServices;
using CompanyManagmentApp.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyManagmentApp.App.ViewModels.Factories
{
    public class ViewModelAbstractFactory : IViewModelAbstractFactory
    {
        private readonly IViewModelFactory<ProjectsViewModel> _projectsViewModelFactory;
        private readonly IViewModelFactory<LoginViewModel> _loginViewModelFactory;
        private readonly IViewModelFactory<ProfileViewModel> _profileViewModelFactory;
        private readonly IViewModelFactory<WorkCardViewModel> _workCardViewModelFactory;
        private readonly IViewModelFactory<DepartmentsViewModel> _departmentsViewModelFactory;
        private readonly IViewModelFactory<JobsViewModel> _jobsViewModelFactory;
        private readonly IViewModelFactory<EmployeesViewModel> _employeesViewModelFactory;
        private readonly IPermissionService permissionsService;
        private readonly IAuthenticator authenticator;

        public ViewModelAbstractFactory(IViewModelFactory<ProjectsViewModel> projectsViewModelFactory,
            IViewModelFactory<LoginViewModel> loginViewModelFactory,
            IViewModelFactory<ProfileViewModel> profileViewModelFactory,
            IViewModelFactory<WorkCardViewModel> workCardViewModelFactory,
            IViewModelFactory<DepartmentsViewModel> departmentsViewModelFactory, 
            IViewModelFactory<JobsViewModel> jobsViewModelFactory,
            IViewModelFactory<EmployeesViewModel> employeesViewModelFactory,
            IPermissionService permissionsService, IAuthenticator authenticator)
        {
            _projectsViewModelFactory = projectsViewModelFactory;
            _loginViewModelFactory = loginViewModelFactory;
            _profileViewModelFactory = profileViewModelFactory;
            _workCardViewModelFactory = workCardViewModelFactory;
            _departmentsViewModelFactory = departmentsViewModelFactory;
            _jobsViewModelFactory = jobsViewModelFactory;
            _employeesViewModelFactory = employeesViewModelFactory;
            this.permissionsService = permissionsService;
            this.authenticator = authenticator;
        }

        public ViewModelBase CreateViewModel(ViewType viewType)
        {
            switch (viewType)
            {
                case ViewType.WorkCard:
                    Task<Permission> taskWC = Task.Run<Permission>(async () => await permissionsService.GetPermissionForModule("Work card", authenticator.CurrentUser.Id));
                    var permissionWC = taskWC.Result;
                    if ((bool)permissionWC.CanRead)
                    {
                        return _workCardViewModelFactory.CreateViewModel();
                    }
                    else
                    {
                        return new NoAccessViewModel();
                    }
                case ViewType.Projects:
                    Task<Permission> taskP = Task.Run<Permission>(async () => await permissionsService.GetPermissionForModule("Project", authenticator.CurrentUser.Id));
                    var permissionP = taskP.Result;
                    if ((bool)permissionP.CanRead)
                    {
                        return _projectsViewModelFactory.CreateViewModel();
                    }
                    else
                    {
                        return new NoAccessViewModel();
                    }
                case ViewType.Profile:
                    return _profileViewModelFactory.CreateViewModel();
                case ViewType.Jobs:
                    Task<Permission> taskJ = Task.Run<Permission>(async () => await permissionsService.GetPermissionForModule("Jobs", authenticator.CurrentUser.Id));
                    var permissionJ = taskJ.Result;
                    if ((bool)permissionJ.CanRead)
                    {
                        return _jobsViewModelFactory.CreateViewModel();
                    }
                    else
                    {
                        return new NoAccessViewModel();
                    }
                case ViewType.Departments:
                    Task<Permission> taskD = Task.Run<Permission>(async () => await permissionsService.GetPermissionForModule("Department", authenticator.CurrentUser.Id));
                    var permissionD = taskD.Result;
                    if ((bool)permissionD.CanRead)
                    {
                        return _departmentsViewModelFactory.CreateViewModel();
                    }
                    else
                    {
                        return new NoAccessViewModel();
                    }
                case ViewType.Employees:
                    Task<Permission> taskE = Task.Run<Permission>(async () => await permissionsService.GetPermissionForModule("Employee", authenticator.CurrentUser.Id));
                    var permissionE = taskE.Result;
                    if ((bool)permissionE.CanRead)
                    {
                        return _employeesViewModelFactory.CreateViewModel();
                    }
                    else
                    {
                        return new NoAccessViewModel();
                    }
                case ViewType.Login:
                    return _loginViewModelFactory.CreateViewModel();
                default:
                    throw new ArgumentException("The view type does not have a view model.", "viewType");
            }
        }
    }
}
