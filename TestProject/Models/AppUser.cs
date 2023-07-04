using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace TestProject.Models
{
    public class AppUser : IdentityUser
    {
        public bool Active { get; set; }
        public Student? Student { get; set; }
    }
}
