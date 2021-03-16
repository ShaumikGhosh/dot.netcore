﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TelephoneApp.Data
{
    public class ApplicationUser : IdentityUser
    {
        [
            PersonalData,
            Column(TypeName = "varchar(100)"),
        ]
        public string FirstName { get; set; }

        [
            PersonalData,
            Column(TypeName = "varchar(100)")
        ]
        public string LastName { get; set; }
    }
}
