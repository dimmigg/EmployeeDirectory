using EmployeeDirectory.DAL.Emtityes.Base;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeDirectory.DAL.Emtityes
{
    public class Employee : Entity
    {
        [Required]
        public string Surname { get; set; }      
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string SecondName { get; set; }
        [Required]
        public DateTime Birthday { get; set; }
        [Required]
        public DateTime EmploymentDate { get; set; }
        [Required]
        public string Position { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        [Required]
        public decimal Wage { get; set; }
        public virtual Department Department { get; set; }
        public virtual int DepartmentId { get; set; }

        public override string ToString() => $"{Surname} {FirstName} {SecondName}";
    }
}
