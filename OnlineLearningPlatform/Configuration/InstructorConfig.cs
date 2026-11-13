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
    internal class InstructorConfig: IEntityTypeConfiguration<Instructor>
    {
        public void Configure(EntityTypeBuilder<Instructor> builder)
    {
        builder.HasKey(i => i.Id);

        builder.Property(i => i.FullName)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(i => i.Email)
               .IsRequired()
               .HasMaxLength(150);

        builder.HasIndex(i => i.Email)
               .IsUnique();

        builder.Property(i => i.Bio)
               .HasMaxLength(500);

        builder.Property(i => i.JoinedDate)
               .IsRequired();

            builder.HasData(
    new Instructor
    {
        Id = 1,
        FullName = "Ahmed",
        Email = "ahmed@gmail.com",
        Bio = "C# Instructor",
        JoinedDate = new DateTime(2025, 1, 1)
    },
    new Instructor
    {
        Id = 2,
        FullName = "Mona",
        Email = "mona@gmail.com",
        Bio = "Database Instructor",
        JoinedDate = new DateTime(2026, 2, 1)
    },
    new Instructor
    {
        Id = 3,
        FullName = "Omar ",
        Email = "omar@gmail.com",
        Bio = null,
        JoinedDate = new DateTime(2024, 7, 1)
    }
);
        }
    
    }
}
