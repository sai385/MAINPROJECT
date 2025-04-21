
using MAINPROJECT.DomaiLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace MAINPROJECT.Datamemoryin_seperate
{
    public class Departmentdata : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.ToTable("Departments");

                builder.HasKey(x => x.Id);
            builder.Property(x =>x.Id).UseIdentityColumn();
            builder.Property(x => x.Course).IsRequired();
            builder.Property(x =>x.DepName).IsRequired();



            builder.HasData(
                new Department
                {
                    Id = 1,
                    Course="B.Tech",
                    DepName="Computers"

                },
                 new Department
                 {
                     Id = 2,
                     Course = "Degree",
                     DepName = "Electrical"

                 });
        }
    }
}
