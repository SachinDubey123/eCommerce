using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.DataLayer
{
    public class DLAdmin
    {
        [Key]
        public int AdminId { get; set; }

        [DisplayName("Username")]
        [Required(ErrorMessage = "*")]
        public string Username { get; set; }

        [DisplayName("Firstname")]
        [Required(ErrorMessage = "*")]
        public string Firstname { get; set; }

        [DisplayName("Lastname")]
        [Required(ErrorMessage = "*")]
        public string  Lastname { get; set; }

        [DisplayName("Gender")]
        [Required(ErrorMessage ="*")]
        public string Gender { get; set; }

        [DisplayName("Mobile")]
        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "*")]
        public string Mobile { get; set; }

        [DisplayName("Email")]
        [Required(ErrorMessage = "*")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Please enter correct email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "*")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DisplayName("Role")]
        [Required(ErrorMessage = "*")]
        public string Role { get; set; }

        [DisplayName("Photo")]
        [Required(ErrorMessage = "*")]
        public string Photo { get; set; }
        public bool Rememberme { get; set; }

        public DateTime CreatedOn { get; set; }
        public DateTime LastLoginDate { get; set; }
    }
    public enum RoleEnum
    {
        Admin,
        Merchant
    }
}
