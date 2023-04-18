using EmployeeDirectory.DAL.Context;
using EmployeeDirectory.DAL.Emtityes;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace EmployeeDirectory.DAL
{
    internal class DepartamentsRepository : DbRepository<Department>
    {
        public DepartamentsRepository(MainDB db) : base(db) { }

        public override IQueryable<Department> Items => base.Items.Include(item => item.Company).Include(item => item.Employees);
    }
}
