using CompanyManagmentApp.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyManagmentApp.App.State.Authenticator
{
    public interface IAuthenticator
    {
        Users CurrentUser { get; }
        bool isLoggedIn { get; }

        Task<bool> Register(Employees employee);
        Task<bool> Login(string username, string password);
        Task<Users> ChangePassword(Users user, string newPassword);
        Task<Users> GetByEmployee(Employees employee);
        void Logout();
    }
}
