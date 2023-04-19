using EmployeeDirectory.DAL.Emtityes;
using MathCore.WPF.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace EmployeeDirectory.ViewModels.Directory.Dialog
{
    class DepartmentEditorViewModel : ViewModel
    {
        public int Id { get; }

        private string _name;
        public string Name
        {
            get => _name;
            set => Set(ref _name, value);
        }
        private Employee _director;
        public Employee Director
        {
            get => _director;
            set => Set(ref _director, value);
        }

        private ICollection<Employee> _employees;
        public ICollection<Employee> Employees
        {
            get => _employees;
            set => Set(ref _employees, value);
        }

        private Company _company;
        public Company Company
        {
            get => _company;
            set => Set(ref _company, value);
        }
        private Employee[] _allEmployees { get; }
        public IEnumerable<Employee> Directors => _allEmployees.Where(e => e.Department.Id == Id);
        public Company[] Companies { get; }

        public DepartmentEditorViewModel(Department department, Company[] companies, Employee[] employees)
        {
            Id = department.Id;
            Name = department.Name;
            Director = department.Director;
            Company = department.Company;
            Employees = department.Employees;
            _allEmployees = employees;
            Companies = companies;
        }
    }
}
