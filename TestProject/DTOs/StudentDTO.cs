using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using TestProject.Models;
using TestProject.Validation;
using WebAppCleanBlog.Validations;

namespace TestProject.DTOs
{
    public class StudentDTO
    {
        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        [Required]
        [StringLength(30, ErrorMessage = "The {0} value cannot exceed {1} characters. ")]
        public string FirstName { get; set; }
        [Required]
        [StringLength(30, ErrorMessage = "The {0} value cannot exceed {1} characters. ")]
        public string FatherName { get; set; }
        [Required]
        [StringLength(30, ErrorMessage = "The {0} value cannot exceed {1} characters. ")]
        public string GrandFatherName { get; set; }
        [Required]
        [StringLength(30, ErrorMessage = "The {0} value cannot exceed {1} characters. ")]
        public string LastName { get; set; }
        [Required]
        [PasswordPropertyText]
        public string Password { get; set; }
        [Required]
        [PasswordPropertyText]
        [Compare("Password", ErrorMessage = "Confirm password doesn't match, Type again !")]
        public string ConfirmPassword { get; set; }
        [Required]
        [GenderValidation]
        public string Gender { get; set; }
        [Required]
        [FileTypeValidation(".jpg,.jpeg,.png", ErrorMessage = "Invalid File type")]
        public IFormFile ProfileImage { get; set; }
        [Required]
        [FileTypeValidation(".jpg,.jpeg,.png", ErrorMessage = "Invalid File type")]
        public IFormFile IDImage { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        [Phone]
        public string Phone { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime EnrollmentDate { get; set; }
        [Required]
        [Range(1, 12, ErrorMessage = "Please enter a value between {1} and {2}")]
        public int Level { get; set; }
        [Required]
        [Range(0, 100, ErrorMessage = "Please enter a value between {1} and {2}")]
        public int LastYearGrade { get; set; }
    }
}
