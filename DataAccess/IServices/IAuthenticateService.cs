using CompanyManagmentApp.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyManagmentApp.DataAccess.IServices
{
    public interface IAuthenticateService
    {
        Task<bool> Register(Employees employee);
        Task<Users> Login(string username, string password);
        Task<Users> ChangePassword(Users user, string newPassword);
        Task<Users> GetAccount(Employees employee);
    }
}
