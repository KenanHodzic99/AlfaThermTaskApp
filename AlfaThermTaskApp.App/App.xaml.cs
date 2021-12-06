using AlfaThermTaskApp.DataAccess.Services;
using AlfaThermTaskApp.App.State.Navigators;
using AlfaThermTaskApp.App.ViewModels;
using AlfaThermTaskApp.App.ViewModels.Factories;
using AlfaThermTaskApp.DataAccess.IServices;
using AlfaThermTaskApp.DatabaseModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using AlfaThermTaskApp.App.State.Authenticator;

namespace AlfaThermTaskApp.App
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            IServiceProvider serviceProvider = CreateServiceProvider();

            Window window = new MainWindow();
            window.DataContext = serviceProvider.GetRequiredService<MainViewModel>();
            window.Show();

            base.OnStartup(e);
        }

        private IServiceProvider CreateServiceProvider()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddScoped<MainViewModel>();
            services.AddSingleton<AlfathermdbContextFactory>();

            services.AddSingleton<IDataService<Projects>,DataService<Projects>>();
            services.AddSingleton<IDataService<Employees>, DataService<Employees>>();
            services.AddSingleton<IDataService<WorkCard>, WorkCardDataService>();
            services.AddSingleton<IDataService<Users>, DataService<Users>>();
            services.AddSingleton<IDataService<Users>, UserDataService>();
            services.AddSingleton<IDataService<Jobs>, JobsDataService>();
            services.AddSingleton<IDataService<Employees>, EmployeeDataService>();
            services.AddSingleton<IDataService<Permission>, PermissionDataService>();


            services.AddSingleton<IDepartmentService, DepartmentService>();
            services.AddSingleton<IPermissionService, PermissionService>();
            services.AddSingleton<IAccountService, AccountService>();
            services.AddSingleton<IAuthenticateService, AuthenticateService>();
            services.AddScoped<INavigator, Navigator>();
            services.AddScoped<IAuthenticator, Authenticator>();

            services.AddSingleton<IViewModelAbstractFactory, ViewModelAbstractFactory>();
            services.AddSingleton<IViewModelFactory<ProjectsViewModel>,ProjectViewModelFactory>();
            services.AddSingleton<IViewModelFactory<WorkCardViewModel>, WorkCardViewModelFactory>();
            services.AddSingleton<IViewModelFactory<ProfileViewModel>, ProfileViewModelFactory>();
            services.AddSingleton<IViewModelFactory<DepartmentsViewModel>, DepartmentsViewModelFactory>();
            services.AddSingleton<IViewModelFactory<JobsViewModel>, JobsViewModelFactory>();
            services.AddSingleton<IViewModelFactory<EmployeesViewModel>, EmployeesViewModelFactory>();
            services.AddSingleton<IViewModelFactory<LoginViewModel>>((service)=> new LoginViewModelFactory(
                service.GetRequiredService<IAuthenticator>(),
                new ViewModelFactoryRenavigator<ProfileViewModel>(
                    service.GetRequiredService<INavigator>(),
                    service.GetRequiredService<IViewModelFactory<ProfileViewModel>>())));


            return services.BuildServiceProvider();
        }
    }
}
