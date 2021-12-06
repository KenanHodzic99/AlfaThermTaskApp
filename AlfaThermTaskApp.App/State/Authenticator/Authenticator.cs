using AlfaThermTaskApp.DataAccess.IServices;
using AlfaThermTaskApp.DatabaseModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlfaThermTaskApp.App.State.Authenticator
{
    public class Authenticator : IAuthenticator, INotifyPropertyChanged
    {
        private IAuthenticateService _authenticateService;

        public Authenticator(IAuthenticateService authenticateService)
        {
            _authenticateService = authenticateService;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private Users _currentUser;
        public Users CurrentUser
        {   
            get { return _currentUser; }
            set { _currentUser = value; OnPropertyChanged(nameof(CurrentUser)); OnPropertyChanged(nameof(isLoggedIn)); }
        }


        public bool isLoggedIn => CurrentUser != null;

        public async Task<bool> Login(string username, string password)
        {
            bool success = false;
            try
            {
                CurrentUser = await _authenticateService.Login(username, password);
                success = true;
            }
            catch (Exception)
            {
                success = false;
            }

            return success;
        }

        public void Logout()
        {
            CurrentUser = null;
        }

        public async Task<bool> Register(Employees employee)
        {
            return await _authenticateService.Register(employee);
        }

        public async Task<Users> ChangePassword(Users user, string newPassword)
        {
            return await _authenticateService.ChangePassword(user, newPassword); ;
        }

        public async Task<Users> GetByEmployee(Employees employee)
        {
            return await _authenticateService.GetAccount(employee);
        }
    }
}
