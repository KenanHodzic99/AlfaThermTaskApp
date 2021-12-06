using AlfaThermTaskApp.DataAccess.IServices;
using AlfaThermTaskApp.DatabaseModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlfaThermTaskApp.DataAccess.Services
{
    public class PermissionService : IPermissionService
    {
        AlfathermdbContextFactory _dbContextFactory;
        public PermissionService(AlfathermdbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<Permission> GetPermissionForModule(string moduleName, int userId)
        {
            using(AlfathermdbContext _dbContext = _dbContextFactory.CreateDbContext())
            {
                var permissions = await _dbContext.Permission.FirstOrDefaultAsync(x => x.ModuleName == moduleName && x.UserId == userId);
                return permissions;
            }
        }

        public async Task<IEnumerable<Permission>> UpdatePermissions(IEnumerable<Permission> newPermissions, int userId)
        {
            using (AlfathermdbContext _dbContext = _dbContextFactory.CreateDbContext())
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
