using MAINPROJECT.Datamemoryin_seperate;
using MAINPROJECT.DomaiLayer;
using Microsoft.EntityFrameworkCore;

namespace MAINPROJECT.AppDbContect
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options):base(options)
        {
            
        }
        public DbSet<Student>Students { get; set; }
        public DbSet<Employee>Employees { get; set; }   
        public DbSet<Department> Departments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new Studentdata());
            modelBuilder.ApplyConfiguration(new Employeedata());
            modelBuilder.ApplyConfiguration(new Departmentdata());
        }
    }
}
