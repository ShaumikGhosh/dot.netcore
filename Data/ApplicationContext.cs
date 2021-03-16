using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TelephoneApp.Models;

namespace TelephoneApp.Data
{
    public class ApplicationContext : IdentityDbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options){}
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<PhonebookModel> PhonebookRecords { get; set; }
    }
}
