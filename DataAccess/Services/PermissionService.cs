using CompanyManagmentApp.DataAccess.IServices;
using CompanyManagmentApp.DatabaseModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyManagmentApp.DataAccess.Services
{
    public class PermissionService : IPermissionService
    {
        CompanyManagmentAppDbContextFactory _dbContextFactory;
        public PermissionService(CompanyManagmentAppDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<Permission> GetPermissionForModule(string moduleName, int userId)
        {
            using(CompanyManagmentAppDbContext _dbContext = _dbContextFactory.CreateDbContext())
            {
                var permissions = await _dbContext.Permission.FirstOrDefaultAsync(x => x.ModuleName == moduleName && x.UserId == userId);
                return permissions;
            }
        }

        public async Task<IEnumerable<Permission>> UpdatePermissions(IEnumerable<Permission> newPermissions, int userId)
        {
            using (CompanyManagmentAppDbContext _dbContext = _dbContextFactory.CreateDbContext())
            {
                var permissions = await _dbContext.Permission.Where(x => x.UserId == userId).ToListAsync();

                foreach (var permission in permissions)
                {
                    foreach (var newPermission in newPermissions)
                    {
                        if(permission.ModuleName == newPermission.ModuleName)
                        {
                            permission.CanDelete = newPermission.CanDelete;
                            permission.CanEdit = newPermission.CanEdit;
                            permission.CanRead = newPermission.CanRead;
                            permission.CanWrite = newPermission.CanWrite;
                        }
                    }
                }

                _dbContext.Permission.UpdateRange(permissions);
                await _dbContext.SaveChangesAsync();
                return permissions;
            }
        }
    }
}
