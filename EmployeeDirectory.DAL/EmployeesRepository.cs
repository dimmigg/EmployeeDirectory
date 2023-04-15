using EmployeeDirectory.DAL.Context;
using EmployeeDirectory.DAL.Emtityes;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace EmployeeDirectory.DAL
{
    internal class EmployeesRepository : DbRepository<Employee>
    {
        public EmployeesRepository(MainDB db) : base(db){ }

        public override IQueryable<Employee> Items => base.Items.Include(item => item.Department);
    }
}
