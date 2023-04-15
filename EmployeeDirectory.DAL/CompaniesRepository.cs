using EmployeeDirectory.DAL.Context;
using EmployeeDirectory.DAL.Emtityes;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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
            foreach (var item in result)
            {
                foreach (var department in item.Departments)
                    department.Director = _db.Set<Employee>().SingleOrDefault(emp => emp.Id == department.DirectorId);
            }
            return result;
        }
    }
}
