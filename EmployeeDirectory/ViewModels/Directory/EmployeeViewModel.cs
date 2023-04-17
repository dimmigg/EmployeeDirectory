using EmployeeDirectory.DAL.Emtityes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EmployeeDirectory.ViewModels.Directory
{
    internal class EmployeeViewModel : TreeBase
    {
        public string Surname { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public DateTime Birthday { get; set; }
        public DateTime EmploymentDate { get; set; }
        public string Position { get; set; }
        public decimal Wage { get; set; }
        public Department Department { get; set; }

        public EmployeeViewModel(Employee employee)
        {
            Surname = employee.Surname;
            FirstName = employee.FirstName;
            SecondName = employee.SecondName;
            Birthday = employee.Birthday;
            EmploymentDate = employee.EmploymentDate;
            Position = employee.Position;
            Wage = employee.Wage;
            Department = employee.Department;
        }
    }
}
