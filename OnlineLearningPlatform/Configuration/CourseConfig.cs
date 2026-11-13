using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineLearningPlatform.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static OnlineLearningPlatform.entities.Course;

namespace OnlineLearningPlatform.Configuration
{
    internal class CourseConfig :
        IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Title)
                   .IsRequired()
                   .HasMaxLength(150);

            builder.Property(c => c.Description)
                   .HasMaxLength(1000);

            builder.Property(c => c.Price)
                   .HasPrecision(10, 2);

            builder.Property(c => c.Level)
                   .IsRequired()
                   .HasConversion<string>();

            builder.Property(c => c.IsPublished)
                   .IsRequired();

            builder.HasOne<Instructor>()
                   .WithMany()
                   .HasForeignKey(c => c.InstructorId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<Category>()
                   .WithMany()
                   .HasForeignKey(c => c.CategoryId)
                   .OnDelete(DeleteBehavior.Restrict);
            builder.HasData(
                new Course
                {
                    Id = 1,
                    Title = "C# Basics",
                    Price = 500,
                    Level = CourseLevel.Beginner,
                    IsPublished = true,
                    PublishedDate = new DateTime(2024, 1, 10),
                    InstructorId = 1,
                    CategoryId = 1
                },
                new Course
                {
                    Id = 2,
                    Title = "OOP with C#",
                    Price = 700,
                    Level = CourseLevel.Intermediate,
                    IsPublished = true,
                    PublishedDate = new DateTime(2024, 2, 10),
                    InstructorId = 1,
                    CategoryId = 2
                },
                new Course
                {
                    Id = 3,
                    Title = "ASP.NET Core",
                    Price = 1000,
                    Level = CourseLevel.Advanced,
                    IsPublished = true,
                    PublishedDate = new DateTime(2024, 3, 10),
                    InstructorId = 2,
                    CategoryId = 3
                },
                new Course
                {
                    Id = 4,
                    Title = "SQL Basics",
                    Price = 400,
                    Level = CourseLevel.Beginner,
                    IsPublished = true,
                    PublishedDate = new DateTime(2024, 4, 10),
                    InstructorId = 2,
                    CategoryId = 4
                },
                new Course
                {
                    Id = 5,
                    Title = "Advanced SQL",
                    Price = 900,
                    Level = CourseLevel.Advanced,
                    IsPublished = false,
                    PublishedDate = null,
                    InstructorId = 3,
                    CategoryId = 5
                },
                new Course
                {
                    Id = 6,
                    Title = "Web Development",
                    Price = 600,
                    Level = CourseLevel.Intermediate,
                    IsPublished = false,
                    PublishedDate = null,
                    InstructorId = 3,
                    CategoryId = 2
                }
            );
        }
    }
}
