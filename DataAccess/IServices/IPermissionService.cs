using CompanyManagmentApp.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyManagmentApp.DataAccess.IServices
{
    public interface IPermissionService
    {
        public Task<Permission> GetPermissionForModule(string moduleName, int userId);

        public Task<IEnumerable<Permission>> UpdatePermissions(IEnumerable<Permission> newPermissions, int userId);  
    }
}
