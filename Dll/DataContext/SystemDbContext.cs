using Dll.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Dll.DataContext
{
    public class SystemDbContext : IdentityDbContext<ApplicationUser>
    {
        public SystemDbContext(DbContextOptions<SystemDbContext> options) : base(options)
        {
        }


        public DbSet<Student> students { get; set; }
        public DbSet<Doctor> doctors { get; set; }
        public DbSet<Subject> subjects { get; set; }
        public DbSet<QRCodeModel> QRCodesTable { get; set; }
        public DbSet<AttendanceTable> attendanceTables { get; set; }

    }
}
