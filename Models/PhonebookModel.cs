using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using TelephoneApp.Data;

namespace TelephoneApp.Models
{
    public class PhonebookModel
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Full name")]
        [Column(TypeName ="varchar(100)")]
        [Required(ErrorMessage ="Full name is required!")]
        public string FullName { get; set; }

        [Display(Name = "Phone number")]
        [Column(TypeName = "varchar(15)")]
        [Required(ErrorMessage = "Phone number is required!")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Address")]
        [Column(TypeName = "varchar(100)")]
        [Required(ErrorMessage = "Address is required!")]
        public string Address { get; set; }

        public bool IsApproved { get; set; } = false;

        [ForeignKey("userId")]
        public ApplicationUser user { get; set; }
    }
}
