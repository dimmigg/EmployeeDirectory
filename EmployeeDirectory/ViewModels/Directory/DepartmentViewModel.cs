using EmployeeDirectory.DAL.Emtityes;
using System.Collections.Generic;

namespace EmployeeDirectory.ViewModels.Directory
{
    internal class DepartmentViewModel : TreeBase
    {
        public string Name { get; set; }
        public int DirectorId { get; set; }
        public Employee Director { get; set; }
        public ICollection<Employee> Employees { get; set; }
        public Company Company { get; set; }

        public DepartmentViewModel(Department department)
        {
            Name = department.Name;
            DirectorId = department.DirectorId;
            Employees = department.Employees;
            Company = department.Company;
        }
    }
}
