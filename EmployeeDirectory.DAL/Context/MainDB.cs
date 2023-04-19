using EmployeeDirectory.DAL.Emtityes;
using Microsoft.EntityFrameworkCore;


namespace EmployeeDirectory.DAL.Context
{
    public class MainDB : DbContext
    {
        public DbSet<Company> Companies { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public MainDB(DbContextOptions<MainDB> options) : base(options) { }        
    }
}
