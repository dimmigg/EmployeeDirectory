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

        public override IQueryable<Company> Items => GetItems(); //base.Items.Include(item => item.Departments).ThenInclude(c => c.Employees);

        private IQueryable<Company> GetItems()
        {
            var result = base.Items.Include(item => item.Departments).ThenInclude(c => c.Employees);
            _ = Task.Run(async () =>
            {
                foreach (var item in result)
            {
                foreach (var department in item.Departments)
                     department.Director = await _db.Set<Employee>().SingleOrDefaultAsync(emp => emp.Id == department.DirectorId).ConfigureAwait(false);
            }
            });
            return result;
        }
    }
}
