using MedServiceAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace MedServiceAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
           // Database.EnsureCreated();  // При первом обращении к БД - этот метод её создаст
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=(LocalDB)\\MSSQLLocalDB;Database=MedService;Trusted_Connection=True;TrustServerCertificate=true");
        }



        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<AppointmentDate> AppointmentDates { get; set;}
        public DbSet<AppointmentTime> AppointmentTimes { get; set;}


      
    }
}
