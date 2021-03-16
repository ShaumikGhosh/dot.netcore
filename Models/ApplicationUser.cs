using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TelephoneApp.Models
{
    public class ApplicationUser : IdentityUser
    {
        [PersonalData, Column(TypeName = "varchar(100)"),]
        public string FirstName { get; set; }

        [PersonalData, Column(TypeName = "varchar(100)")]
        public string LastName { get; set; }

        [PersonalData, Column(TypeName = "varchar(20)")]
        public string UserType { get; set; }
    }
}
