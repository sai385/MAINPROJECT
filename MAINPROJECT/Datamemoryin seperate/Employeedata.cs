using MAINPROJECT.DomaiLayer;
using MAINPROJECT.ModelDto;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MAINPROJECT.Datamemoryin_seperate
{
    public class Employeedata : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable("Employees");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).UseIdentityColumn();
            builder.Property(e=>e.Name).IsRequired();
            builder.Property(e =>e.Salary).IsRequired();
            builder.Property(e =>e.Gender).IsRequired();


            builder.HasData(
                new Employee
                {
                    Id = 1,
                    Name = "vinay",
                    Gender = "Male",
                    Salary = 34334.83

                },
                 new Employee
                 {
                     Id = 2,
                     Name = "pavani",
                     Gender = "Female",
                     Salary = 34365.83

                 });

            builder.HasOne(e => e.Student)
            .WithMany(d => d.Employees)
            .HasForeignKey(e => e.StudentId)
            .OnDelete(DeleteBehavior.Cascade);

            
        }
    }
}
