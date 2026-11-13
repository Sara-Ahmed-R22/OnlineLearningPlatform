using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearningPlatform.entities
{
    internal class Course
    {
        public enum CourseLevel
        {
            Beginner,
            Intermediate,
            Advanced
        }
        public int Id { get; set; }

        public string Title { get; set; }

        public string? Description { get; set; }

        public decimal Price { get; set; }

        public CourseLevel Level { get; set; }

        public bool IsPublished { get; set; }

        public DateTime? PublishedDate { get; set; }

        public int InstructorId { get; set; }

        public int CategoryId { get; set; }
    }
}
