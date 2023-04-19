using EmployeeDirectory.DAL.Emtityes.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeDirectory.DAL.Emtityes
{
    public class Department : Entity
    {
        [Required]
        public string Name { get; set; }
        public int DirectorId { get; set; }
        [NotMapped]
        public Employee Director { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
        public virtual Company Company { get; set; }
        public virtual int CompanyId { get; set; }

        public override string ToString() => Name;
    }
}
