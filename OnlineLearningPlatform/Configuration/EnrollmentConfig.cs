using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineLearningPlatform.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearningPlatform.Configuration
{
    internal class EnrollmentConfig :
        IEntityTypeConfiguration<Enrollment>
    {
        public void Configure(EntityTypeBuilder<Enrollment> builder)
        {
            builder.HasKey(e => new
            {
                e.StudentId,
                e.CourseId
            });

            builder.Property(e => e.Progress)
                   .HasPrecision(5, 2);

            builder.Property(e => e.Status)
                   .IsRequired()
                   .HasConversion<string>();

            builder.Property(e => e.FinalGrade)
                   .HasPrecision(5, 2);

            builder.Property(e => e.EnrollmentDate)
                   .IsRequired();

            builder.HasOne<Student>()
                   .WithMany()
                   .HasForeignKey(e => e.StudentId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne<Course>()
                   .WithMany()
                   .HasForeignKey(e => e.CourseId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
