using EmployeeDirectory.DAL.Context;
using EmployeeDirectory.DAL.Emtityes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmployeeDirectory.DAL
{
    internal class DepartamentsRepository : DbRepository<Department>
    {
        private readonly MainDB _db;

        public DepartamentsRepository(MainDB db) : base(db)
        {
            _db = db;
        }

        public override IQueryable<Department> Items => GetItems();

        private IQueryable<Department> GetItems()
        {
            var result = base.Items.Include(item => item.Company).Include(item => item.Employees);
            foreach (var item in result)
            {
                item.Director = _db.Set<Employee>().SingleOrDefault(emp => emp.Id == item.DirectorId);
            }
            return result;
        }
    }
}
