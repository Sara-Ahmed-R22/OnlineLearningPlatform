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
    internal class CategoryConfig :
        IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(c => c.Description)
                   .HasMaxLength(500);

            builder.HasOne<Category>()
                   .WithMany()
                   .HasForeignKey(c => c.ParentCategoryId)
                   .OnDelete(DeleteBehavior.Restrict);


            builder.HasData(
    new Category
    {
        Id = 1,
        Name = "Programming",
        ParentCategoryId = null
    },
    new Category
    {
        Id = 2,
        Name = "Backend",
        ParentCategoryId = 1
    },
    new Category
    {
        Id = 3,
        Name = "ASP.NET",
        ParentCategoryId = 2
    },
    new Category
    {
        Id = 4,
        Name = "Database",
        ParentCategoryId = 1
    },
    new Category
    {
        Id = 5,
        Name = "SQL Server",
        ParentCategoryId = 4
    }
);
        }
    }
}
