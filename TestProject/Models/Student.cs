using System.ComponentModel.DataAnnotations;

namespace TestProject.Models
{
    public class Student
    {
        // OutofMap
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string FatherName { get; set; }
        public string GrandFatherName { get; set; }
        public string LastName { get; set; }
        
        public string Gender { get; set; }
        // OutofMap
        public string ProfileImageUrl { get; set; }
        // OutofMap
        public string IDImageUrl { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public int Level { get; set; }
        public int LastYearGrade { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        // OutofMap
        public string? UserId { get; set; }
        // OutofMap
        public AppUser? User { get; set; }

    }
}
