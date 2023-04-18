using EmployeeDirectory.DAL.Emtityes;
using MathCore.WPF.ViewModels;
using System;
using System.Collections.Generic;

namespace EmployeeDirectory.ViewModels.Directory.Dialog
{
    class CompanyEditorViewModel : ViewModel
    {
        public int Id { get; }

        private string _name;
        public string Name 
        {
            get => _name;
            set => Set(ref _name, value);
        }

        private string _address;
        public string Address
        {
            get => _address;
            set => Set(ref _address, value);
        }

        private DateTime _created;
        public DateTime Created
        {
            get => _created;
            set => Set(ref _created, value);
        }

        private ICollection<Department> _departments;
        public ICollection<Department> Departments
        {
            get => _departments;
            set => Set(ref _departments, value);
        }     

        public CompanyEditorViewModel(Company company )
        {
            Id = company.Id;
            Name = company.Name;
            Address = company.Address;
            Created = company.Created;
            Departments = company.Departments;
        }
    }
}
