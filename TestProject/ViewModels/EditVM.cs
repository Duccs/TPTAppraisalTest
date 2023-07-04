using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using TestProject.Validation;
using WebAppCleanBlog.Validations;

namespace TestProject.ViewModels
{
    public class EditVM
    {
        [Required]
        public int Id { get; set; }
        [StringLength(30, ErrorMessage = "The {0} value cannot exceed {1} characters. ")]
        public string FirstName { get; set; }
        [StringLength(30, ErrorMessage = "The {0} value cannot exceed {1} characters. ")]
        public string FatherName { get; set; }
        [StringLength(30, ErrorMessage = "The {0} value cannot exceed {1} characters. ")]
        public string GrandFatherName { get; set; }
        [StringLength(30, ErrorMessage = "The {0} value cannot exceed {1} characters. ")]
        public string LastName { get; set; }
        [GenderValidation]
        public string Gender { get; set; }
        [FileTypeValidation(".jpg,.jpeg,.png", ErrorMessage = "Invalid File type")]
        public IFormFile? ProfileImage { get; set; }
        public string ProfileImageUrl { get; set; }
        [FileTypeValidation(".jpg,.jpeg,.png", ErrorMessage = "Invalid File type")]
        public IFormFile? IDImage { get; set; }
        public string IDImageUrl { get; set; }

        public string Address { get; set; }
        [Phone]
        public string Phone { get; set; }
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime EnrollmentDate { get; set; }
        [Range(1, 12, ErrorMessage = "Please enter a value between {1} and {2}")]
        public int Level { get; set; }
        [Range(0, 100, ErrorMessage = "Please enter a value between {1} and {2}")]
        public int LastYearGrade { get; set; }
        public string UserId { get; set; }
    }
}
