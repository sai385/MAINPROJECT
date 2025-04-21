using MAINPROJECT.DomaiLayer;
using MAINPROJECT.ModelDto;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Immutable;

namespace MAINPROJECT.Datamemoryin_seperate
{
    public class Studentdata : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.ToTable("Students");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Age).IsRequired();
            builder.Property(x =>x.Gender).IsRequired();

            builder.HasData(
                new Student
                {
                    Id = 1,
                    Name="SAI VINAY",
                    Age=22,
                    Gender="MALE"

                },
                new Student
                {
                    Id = 2,
                    Name = "thara",
                    Age = 21,
                    Gender = "Female"

                });
        }
    }
}
