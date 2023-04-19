using EmployeeDirectory.DAL.Emtityes;
using MathCore.WPF.ViewModels;
using System;

namespace EmployeeDirectory.ViewModels.Directory.Dialog
{
    class EmployeeEditorViewModel : ViewModel
    {
        public int Id { get; }

        private string _surname;
        public string Surname
        {
            get => _surname;
            set => Set(ref _surname, value);
        }
        private string _firstName;
        public string FirstName
        {
            get => _firstName;
            set => Set(ref _firstName, value);
        }
        private string _secondName;
        public string SecondName
        {
            get => _secondName;
            set => Set(ref _secondName, value);
        }
        private DateTime _birthday;
        public DateTime Birthday
        {
            get => _birthday;
            set => Set(ref _birthday, value);
        }
        private DateTime _employmentDate;
        public DateTime EmploymentDate
        {
            get => _employmentDate;
            set => Set(ref _employmentDate, value);
        }
        private string _position;
        public string Position
        {
            get => _position;
            set => Set(ref _position, value);
        }
        private decimal _wage;
        public decimal Wage
        {
            get => _wage;
            set => Set(ref _wage, value);
        }
        private Department _department;
        public Department Department
        {
            get => _department;
            set => Set(ref _department, value);
        }

        public Department[] Departments { get; }

        public EmployeeEditorViewModel(Employee employee, Department[] departments)
        {
            Id = employee.Id;
            Surname = employee.Surname;
            FirstName = employee.FirstName;
            SecondName = employee.SecondName;
            Birthday = employee.Birthday;
            EmploymentDate = employee.EmploymentDate;
            Position = employee.Position;
            Wage = employee.Wage;
            Department = employee.Department;
            Departments = departments;
        }
    }
}
