using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlfaThermTaskApp.DatabaseModels
{
    public class Departments
    {
        public Departments()
        {
            Jobs = new HashSet<Jobs>();
        }
        public HierarchyId Id { get; set; }
        public string DepartmentName { get; set; }

        public virtual ICollection<Jobs> Jobs { get; set; }
    }
}
