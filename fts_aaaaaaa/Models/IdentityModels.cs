using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace fts_aaaaaaa.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("Connection1", throwIfV1Schema: false)
        {
        }

        public DbSet<MeasureResult> MeasureResults { get; set; }
        public DbSet<Measure> Measures { get; set; }
        public DbSet<BloodPressure> BloodPressures { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        
    }

    public class MeasureResult
    {
        public int Id { get; set; }

        public string UserId { get; set; }
        public int MeasureId { get; set; }

        [Required(ErrorMessage = "Дані повинні бути введені")]
        public double Value { get; set; }

        [Required(ErrorMessage = "Дані повинні бути введені")]
        [DataType (DataType.Date)]
        public DateTime CreatedDate { get; set; }

        public virtual ApplicationUser User { get; set; }
        public virtual Measure Measure { get; set; }
    }

    public class Measure
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Unit { get; set; }

    }

    public class BloodPressure
    {
        public int Id { get; set; }
        public string UserId { get; set; }

        [Required(ErrorMessage = "Дані повинні бути введені")]
        public int UpPressure { get; set; }
        [Required(ErrorMessage = "Дані повинні бути введені")]
        public int LowPressure { get; set; }

        [Required(ErrorMessage = "Дані повинні бути введені")]
        [DataType(DataType.Date)]
        public DateTime CreatedDate { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}