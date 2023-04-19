using EmployeeDirectory.DAL.Context;
using EmployeeDirectory.DAL.Emtityes;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace EmployeeDirectory.DAL
{
    internal class CompaniesRepository : DbRepository<Company>
    {
        public CompaniesRepository(MainDB db) : base(db) { }

        public override IQueryable<Company> Items => base.Items.Include(item => item.Departments).ThenInclude(c => c.Employees);
    }
}
