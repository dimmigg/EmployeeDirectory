using EmployeeDirectory.DAL.Emtityes;
using EmployeeDirectory.DAL.Emtityes.Base;
using EmployeeDirectory.Interfaces;
using EmployeeDirectory.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace EmployeeDirectory.Services
{
    internal class EditItemsService : IEditItemsService
    {
        private readonly IRepository<Department> _departmentRepo;
        private readonly IRepository<Company> _companyRepo;
        private readonly IRepository<Employee> _employeeRepo;

        public EditItemsService(IRepository<Department> departmentRepo,
                                IRepository<Company> companyRepo,
                                IRepository<Employee> employeeRepo)
        {
            _departmentRepo = departmentRepo;
            _companyRepo = companyRepo;
            _employeeRepo = employeeRepo;
        }

        public async Task<Entity> AddItemAsync(Entity entity)
        {
            throw new Exception();
        }
    }
}
