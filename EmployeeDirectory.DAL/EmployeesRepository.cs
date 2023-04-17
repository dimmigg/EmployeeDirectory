using EmployeeDirectory.DAL.Context;
using EmployeeDirectory.DAL.Emtityes;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeDirectory.DAL
{
    internal class EmployeesRepository : DbRepository<Employee>
    {
        private readonly MainDB _db;

        public EmployeesRepository(MainDB db) : base(db)
        {
            _db = db;
        }

        public override IQueryable<Employee> Items => GetItems(); //base.Items.Include(item => item.Departments).ThenInclude(c => c.Employees);

        private IQueryable<Employee> GetItems()
        {
            var result = base.Items.Include(item => item.Department);
            _ = Task.Run(async () =>
            {
                foreach (var item in result)
            { 
                    item.Department.Director = await Items.SingleOrDefaultAsync(emp => emp.Id == item.Department.DirectorId).ConfigureAwait(false);
            }
            });
            return result;
        }
    }
}
