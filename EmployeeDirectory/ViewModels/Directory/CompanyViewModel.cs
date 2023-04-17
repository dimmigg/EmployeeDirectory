using EmployeeDirectory.DAL.Emtityes;
using MathCore.WPF.ViewModels;
using System.Collections.Generic;
using System;
using System.Linq;

namespace EmployeeDirectory.ViewModels.Directory
{
    internal class CompanyViewModel : TreeBase
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public DateTime Created { get; set; }
        public ICollection<DepartmentViewModel> Departments { get; set; }
        
        public CompanyViewModel(Company company)
        {
            Name = company.Name;
            Address = company.Address;
            Created = company.Created;
            Departments = company.Departments.Select(d => new DepartmentViewModel(d)).ToList();
        }
    }
}
