using CompanyManagmentApp.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyManagmentApp.DataAccess.IServices
{
    public interface IAccountService : IDataService<Users>
    {
        Task<Users> GetByUsername(string username);
        Task<Users> GetByEmployee(Employees employee);
    }
}
