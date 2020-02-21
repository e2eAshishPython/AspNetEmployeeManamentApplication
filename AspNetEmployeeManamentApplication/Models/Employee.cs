using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetEmployeeManamentApplication.Models
{
    public class Employee
    {
        public int ID { get; set; }
        [Required]
        [MaxLength(50,ErrorMessage ="Can Not Enter More then 50 Character")]
        public string Name { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$",ErrorMessage ="Please Enter Valid Email")]
        [Display(Name ="Office Email")]
        public string Email { get; set; }
        [Required]
        public Dept? Department { get; set; }
        public string PhotoPath { get; set; }
    }
}
