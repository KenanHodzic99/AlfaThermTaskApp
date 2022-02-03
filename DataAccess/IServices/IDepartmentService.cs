using CompanyManagmentApp.DatabaseModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyManagmentApp.DataAccess.IServices
{
    public interface IDepartmentService
    {
        Task<Departments> Create(Departments newDepartment, Departments ancestor);

        Task<Departments> CreateRoot(Departments departments);

        Task<Departments> GetById(HierarchyId Id);

        Task<bool> Delete(HierarchyId Id);

        Task<Departments> Update(HierarchyId Id, Departments entity);

        Task<IEnumerable<Departments>> GetAll();
    }
}
