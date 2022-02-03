using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyManagmentApp.DataAccess.IServices;
using CompanyManagmentApp.DatabaseModels;
using Microsoft.EntityFrameworkCore;

namespace CompanyManagmentApp.DataAccess.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly CompanyManagmentAppDbContextFactory _dbContextFactory;

        public DepartmentService(CompanyManagmentAppDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }


        public async Task<Departments> Create(Departments newDepartment, Departments ancestor)
        {
            using (CompanyManagmentAppDbContext _dbContext = _dbContextFactory.CreateDbContext())
            {
                var departments = await GetAll();

                int counter = 1;

                foreach (var department in departments)
                {
                    if (department.Id != HierarchyId.GetRoot())
                    {
                        if (department.Id.GetAncestor(1) == ancestor.Id)
                        {
                            counter++;
                        }
                    }
                }

                Departments departmentToAdd = new Departments
                {
                    Id = HierarchyId.Parse($"{ancestor.Id}{counter}/"),
                    DepartmentName = newDepartment.DepartmentName
                };
                _dbContext.Departments.Add(departmentToAdd);
                await _dbContext.SaveChangesAsync();
                return newDepartment;
            }
        }

        public async Task<Departments> CreateRoot(Departments departments)
        {
            using (CompanyManagmentAppDbContext _dbContext = _dbContextFactory.CreateDbContext())
            {
                Departments newDepartment = new Departments
                {
                    DepartmentName = departments.DepartmentName,
                };

                int counter = 1;

                foreach(var department in await GetAll())
                {
                    if (department.Id.GetLevel() == 1)
                        counter++;
                }

                newDepartment.Id = HierarchyId.Parse($"/{counter}/");

                _dbContext.Departments.Add(newDepartment);
                await _dbContext.SaveChangesAsync();

                return newDepartment;
            }
        }

        public async Task<bool> Delete(HierarchyId Id)
        {
            using (CompanyManagmentAppDbContext _dbContext = _dbContextFactory.CreateDbContext())
            {
                List<Departments> toDelete = new List<Departments>();
                var departments = await GetAll();

                foreach(var department in departments)
                {
                    if (department.Id.ToString().StartsWith(Id.ToString()))
                    {
                        toDelete.Add(department);
                    }
                }
                
                if (toDelete.Any())
                {
                    _dbContext.RemoveRange(toDelete);
                    await _dbContext.SaveChangesAsync();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public async Task<IEnumerable<Departments>> GetAll()
        {
            using (CompanyManagmentAppDbContext _dbContext = _dbContextFactory.CreateDbContext())
            {
                return await _dbContext.Departments.ToListAsync();
            }
        }

        public async Task<Departments> GetById(HierarchyId Id)
        {
            using (CompanyManagmentAppDbContext _dbContext = _dbContextFactory.CreateDbContext())
            {
                return await _dbContext.Departments.FirstOrDefaultAsync(x => x.Id == Id);
            }
        }

        public async Task<Departments> Update(HierarchyId Id, Departments entity)
        {
            using (CompanyManagmentAppDbContext _dbContext = _dbContextFactory.CreateDbContext())
            {
                entity.Id = Id;
                var newEntity =  _dbContext.Departments.Update(entity);
                await _dbContext.SaveChangesAsync();
                return newEntity.Entity;
            }
        }
    }
}
