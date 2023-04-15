using EmployeeDirectory.DAL.Emtityes.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EmployeeDirectory.DAL.Emtityes
{
    public class Company : Entity
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public DateTime Created { get; set; }
        public virtual ICollection<Department> Departments { get; set; }
    }
}
