using EmployeeDirectory.DAL.Context;
using EmployeeDirectory.DAL.Emtityes;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeDirectory.DAL
{
    internal class CompaniesRepository : DbRepository<Company>
    {
        private readonly MainDB _db;

        public CompaniesRepository(MainDB db) : base(db) 
        {
            _db = db;
        }

        public override IQueryable<Company> Items => base.Items.Include(item => item.Departments).ThenInclude(c => c.Employees);
    }
}
