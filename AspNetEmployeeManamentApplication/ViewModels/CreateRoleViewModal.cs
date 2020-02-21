using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetEmployeeManamentApplication.ViewModels
{
    public class CreateRoleViewModal
    {
        [Required]
        public string RoleName { get; set; }
    }
}
