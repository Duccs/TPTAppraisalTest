using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using TestProject.Models;

namespace TestProject.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Student> Student { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Student>()
                .HasOne(x => x.User)
                .WithOne(x => x.Student)
                .HasForeignKey<Student>(x => x.UserId)
                .IsRequired();
            base.OnModelCreating(builder);
        }
    }
}