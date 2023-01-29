using Microsoft.EntityFrameworkCore;
using ODOL.Models;


namespace ODOL.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
        }

        public DbSet<Pengguna> Pengguna { get; set; }
        public DbSet<Perusahaan> Perusahaan { get; set; }
        
        public DbSet<Mahasiswa> Mahasiswa { get; set; }

        public DbSet<Pembimbing> Pembimbing { get; set; }
        public DbSet<LogBook> LogBook { get; set; }
    }
}
