
using Microsoft.EntityFrameworkCore;
using OnlineLearningPlatform.context;
using OnlineLearningPlatform.entities;
using static OnlineLearningPlatform.entities.Course;
using static OnlineLearningPlatform.entities.Enrollment;

namespace OnlineLearningPlatform
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using appContext db = new appContext();

          
            SeedData(db);

            
            Console.WriteLine("===== CREATE =====");

            AddInstructor(db);
            AddCourse(db);
            AddSeveralStudents(db);
            EnrollStudent(db);

         
            Console.WriteLine("===== READ =====");

            FindCourse(db);
            FilterCourses(db);
            ShowSubCategories(db);
            ShowCourseRoster(db);

           
            Console.WriteLine("===== UPDATE =====");

            ChangeCoursePrice(db);
            UpdateProgress(db);
            PublishCourse(db);

         
            Console.WriteLine("===== DELETE =====");

            UnenrollStudent(db);
            TryDeleteCategory(db);
            SafeDeleteCategory(db);
            DeleteCourse(db);

         
            Console.WriteLine("===== BULK OPERATIONS =====");

            BulkPriceRaise(db);
            BulkDelete(db);
        }

        

        static void SeedData(appContext db)
        {
           
            if (!db.Students.Any())
            {
                var students = new List<Student>
                {
                    new Student
                    {
                        FullName = "Ali Ahmed",
                        Email = "ali@gmail.com",
                        DateOfBirth = new DateTime(2000, 1, 1)
                    },
                    new Student
                    {
                        FullName = "Sara Ahmed",
                        Email = "sara@gmail.com",
                        DateOfBirth = new DateTime(2001, 2, 2)
                    },
                    new Student
                    {
                        FullName = "Omar Ali",
                        Email = "omar2@gmail.com",
                        DateOfBirth = new DateTime(2002, 3, 3)
                    },
                    new Student
                    {
                        FullName = "Mona Hassan",
                        Email = "mona2@gmail.com",
                        DateOfBirth = new DateTime(2000, 4, 4)
                    },
                    new Student
                    {
                        FullName = "Ahmed Samir",
                        Email = "ahmed2@gmail.com",
                        DateOfBirth = new DateTime(2001, 5, 5)
                    }
                };

                db.Students.AddRange(students);
                db.SaveChanges();

                Console.WriteLine("Students Seeded:");

                foreach (var student in students)
                {
                    Console.WriteLine(
                        $"{student.Id} - {student.FullName}");
                }
            }

           
            if (!db.Enrollments.Any())
            {
                var students = db.Students
                    .OrderBy(s => s.Id)
                    .ToList();

            
                if (students.Count < 5)
                {
                    Console.WriteLine(
                        "Cannot seed enrollments. At least 5 students are required.");
                    return;
                }

                var enrollments = new List<Enrollment>
                {
                    new Enrollment
                    {
                        StudentId = students[0].Id,
                        CourseId = 1,
                        EnrollmentDate = DateTime.Now,
                        Progress = 50,
                        Status = EnrollmentStatus.Active
                    },
                    new Enrollment
                    {
                        StudentId = students[0].Id,
                        CourseId = 2,
                        EnrollmentDate = DateTime.Now,
                        Progress = 100,
                        Status = EnrollmentStatus.Completed,
                        FinalGrade = 95
                    },
                    new Enrollment
                    {
                        StudentId = students[1].Id,
                        CourseId = 1,
                        EnrollmentDate = DateTime.Now,
                        Progress = 30,
                        Status = EnrollmentStatus.Active
                    },
                    new Enrollment
                    {
                        StudentId = students[1].Id,
                        CourseId = 3,
                        EnrollmentDate = DateTime.Now,
                        Progress = 100,
                        Status = EnrollmentStatus.Completed,
                        FinalGrade = 90
                    },
                    new Enrollment
                    {
                        StudentId = students[2].Id,
                        CourseId = 2,
                        EnrollmentDate = DateTime.Now,
                        Progress = 20,
                        Status = EnrollmentStatus.Dropped
                    },
                    new Enrollment
                    {
                        StudentId = students[2].Id,
                        CourseId = 4,
                        EnrollmentDate = DateTime.Now,
                        Progress = 60,
                        Status = EnrollmentStatus.Active
                    },
                    new Enrollment
                    {
                        StudentId = students[3].Id,
                        CourseId = 3,
                        EnrollmentDate = DateTime.Now,
                        Progress = 100,
                        Status = EnrollmentStatus.Completed,
                        FinalGrade = 88
                    },
                    new Enrollment
                    {
                        StudentId = students[4].Id,
                        CourseId = 1,
                        EnrollmentDate = DateTime.Now,
                        Progress = 10,
                        Status = EnrollmentStatus.Dropped
                    }
                };

                db.Enrollments.AddRange(enrollments);
                db.SaveChanges();

                Console.WriteLine("8 Enrollments Seeded.");
            }
        }

        static void AddInstructor(appContext db)
        {
            bool exists = db.Instructors.Any(
                i => i.Email == "mostafa@gmail.com");

            if (exists)
            {
                Console.WriteLine("Instructor already exists.");
                return;
            }

            var instructor = new Instructor
            {
                FullName = "Mostafa Ali",
                Email = "mostafa@gmail.com",
                Bio = "Backend Developer",
                JoinedDate = DateTime.Now
            };

            db.Instructors.Add(instructor);
            db.SaveChanges();

            Console.WriteLine(
                $"Instructor added. ID: {instructor.Id}");
        }

        static void AddCourse(appContext db)
        {
            bool exists = db.Courses.Any(
                c => c.Title == "Entity Framework Core");

            if (exists)
            {
                Console.WriteLine("Course already exists.");
                return;
            }

            int instructorId = 1;
            int categoryId = 1;

            var instructor = db.Instructors.Find(instructorId);
            var category = db.Categories.Find(categoryId);

            if (instructor == null)
            {
                Console.WriteLine("Instructor not found.");
                return;
            }

            if (category == null)
            {
                Console.WriteLine("Category not found.");
                return;
            }

            var course = new Course
            {
                Title = "Entity Framework Core",
                Description = "Learn EF Core",
                Price = 800,
                Level = CourseLevel.Intermediate,
                IsPublished = false,
                PublishedDate = null,
                InstructorId = instructorId,
                CategoryId = categoryId
            };

            db.Courses.Add(course);
            db.SaveChanges();

            Console.WriteLine($"Course added. ID: {course.Id}");
        }

        static void AddSeveralStudents(appContext db)
        {
            var students = new List<Student>();

            if (!db.Students.Any(s => s.Email == "student1@gmail.com"))
            {
                students.Add(new Student
                {
                    FullName = "Student One",
                    Email = "student1@gmail.com",
                    DateOfBirth = new DateTime(2000, 1, 1)
                });
            }

            if (!db.Students.Any(s => s.Email == "student2@gmail.com"))
            {
                students.Add(new Student
                {
                    FullName = "Student Two",
                    Email = "student2@gmail.com",
                    DateOfBirth = new DateTime(2001, 1, 1)
                });
            }

            if (!db.Students.Any(s => s.Email == "student3@gmail.com"))
            {
                students.Add(new Student
                {
                    FullName = "Student Three",
                    Email = "student3@gmail.com",
                    DateOfBirth = new DateTime(2002, 1, 1)
                });
            }

            if (students.Count == 0)
            {
                Console.WriteLine("Students already exist.");
                return;
            }

            db.Students.AddRange(students);
            db.SaveChanges();

            foreach (var student in students)
            {
                Console.WriteLine(
                    $"{student.FullName} - ID: {student.Id}");
            }
        }

        static void EnrollStudent(appContext db)
        {
            var student = db.Students.FirstOrDefault(
                s => s.Email == "student1@gmail.com");

            int courseId = 1;

            if (student == null)
            {
                Console.WriteLine("Student not found.");
                return;
            }

            int studentId = student.Id;

            var course = db.Courses.Find(courseId);

            if (course == null)
            {
                Console.WriteLine("Course not found.");
                return;
            }

            if (!course.IsPublished)
            {
                Console.WriteLine(
                    "Cannot enroll. Course is not published.");
                return;
            }

            bool alreadyEnrolled = db.Enrollments.Any(e =>
                e.StudentId == studentId &&
                e.CourseId == courseId);

            if (alreadyEnrolled)
            {
                Console.WriteLine("Student is already enrolled.");
                return;
            }

            var enrollment = new Enrollment
            {
                StudentId = studentId,
                CourseId = courseId,
                EnrollmentDate = DateTime.Now,
                Progress = 0,
                Status = EnrollmentStatus.Active
            };

            db.Enrollments.Add(enrollment);
            db.SaveChanges();

            Console.WriteLine("Enrollment added successfully.");
        }


        static void FindCourse(appContext db)
        {
            var course = db.Courses.Find(1);

            if (course == null)
                Console.WriteLine("Course not found.");
            else
                Console.WriteLine($"Course: {course.Title}");

            var courseByTitle = db.Courses
            .FirstOrDefault(c => c.Title == "SQL Basics");

            if (courseByTitle == null)
                Console.WriteLine("Course not found.");
            else
                Console.WriteLine($"Course: {courseByTitle.Title}");
        }

        static void FilterCourses(appContext db)
        {
            decimal minPrice = 400;
            decimal maxPrice = 1200;

            var courses = db.Courses
                .Where(c => c.Price >= minPrice &&
                            c.Price <= maxPrice)
                .OrderByDescending(c => c.Price)
                .Select(c => new
                {
                    c.Title,
                    c.Level,
                    c.Price
                })
                .ToList();

            foreach (var course in courses)
            {
                Console.WriteLine(
                    $"{course.Title} | {course.Level} | {course.Price}");
            }
        }

        static void ShowSubCategories(appContext db)
        {
            int categoryId = 1;

            var subCategories = db.Categories
                .Where(c => c.ParentCategoryId == categoryId)
                .ToList();

            Console.WriteLine("Subcategories:");

            foreach (var category in subCategories)
            {
                Console.WriteLine(category.Name);
            }

            var topCategories = db.Categories
                .Where(c => c.ParentCategoryId == null)
                .ToList();

            Console.WriteLine("Top-level Categories:");

            foreach (var category in topCategories)
            {
                Console.WriteLine(category.Name);
            }
        }

        static void ShowCourseRoster(appContext db)
        {
            int courseId = 1;

            var studentIds = db.Enrollments
                .Where(e => e.CourseId == courseId)
                .Select(e => e.StudentId)
                .ToList();

            var students = db.Students
                .Where(s => studentIds.Contains(s.Id))
                .ToList();

            Console.WriteLine(
                $"Number of students: {students.Count}");

            foreach (var student in students)
            {
                Console.WriteLine(student.FullName);
            }
        }


        static void ChangeCoursePrice(appContext db)
        {
            var course = db.Courses.Find(1);

            if (course == null)
            {
                Console.WriteLine("Course not found.");
                return;
            }

            course.Price = 1200;

            db.SaveChanges();

            Console.WriteLine("Course price updated.");
        }

        static void UpdateProgress(appContext db)
        {
            int studentId = 1;
            int courseId = 1;

            var enrollment = db.Enrollments.Find(
                studentId, courseId);

            if (enrollment == null)
            {
                Console.WriteLine("Enrollment not found.");
                return;
            }

            decimal newProgress = 100;

            if (newProgress < 0 || newProgress > 100)
            {
                Console.WriteLine(
                    "Progress must be between 0 and 100.");
                return;
            }

            enrollment.Progress = newProgress;

            if (newProgress == 100)
            {
                enrollment.Status = EnrollmentStatus.Completed;
            }

            db.SaveChanges();

            Console.WriteLine("Progress updated.");
        }

        static void PublishCourse(appContext db)
        {
            var course = db.Courses.Find(5);

            if (course == null)
            {
                Console.WriteLine("Course not found.");
                return;
            }

            course.IsPublished = true;
            course.PublishedDate = DateTime.Now;

            db.SaveChanges();

            Console.WriteLine("Course published successfully.");
        }

        
        static void UnenrollStudent(appContext db)
        {
            int studentId = 2;
            int courseId = 1;

            var enrollment = db.Enrollments.Find(
                studentId, courseId);

            if (enrollment == null)
            {
                Console.WriteLine("Enrollment not found.");
                return;
            }

            db.Enrollments.Remove(enrollment);
            db.SaveChanges();

            Console.WriteLine("Student unenrolled successfully.");
        }

        static void TryDeleteCategory(appContext db)
        {
            var category = db.Categories.Find(1);

            if (category == null)
            {
                Console.WriteLine("Category not found.");
                return;
            }

            db.Categories.Remove(category);

            try
            {
                db.SaveChanges();
                Console.WriteLine("Category deleted.");
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine("Cannot delete category.");

                Console.WriteLine(
                    ex.InnerException?.Message ?? ex.Message);

                db.Entry(category).State = EntityState.Unchanged;
            }
        }

        static void SafeDeleteCategory(appContext db)
        {
            int categoryId = 1;

            var category = db.Categories.Find(categoryId);

            if (category == null)
            {
                Console.WriteLine("Category not found.");
                return;
            }

            bool hasSubCategories = db.Categories
                .Any(c => c.ParentCategoryId == categoryId);

            if (hasSubCategories)
            {
                Console.WriteLine(
                    "Cannot delete category because it has sub-categories.");
                return;
            }

            db.Categories.Remove(category);
            db.SaveChanges();

            Console.WriteLine("Category deleted.");
        }

        static void DeleteCourse(appContext db)
        {
            int courseId = 2;

            var course = db.Courses.Find(courseId);

            if (course == null)
            {
                Console.WriteLine("Course not found.");
                return;
            }

            int before = db.Enrollments
                .Count(e => e.CourseId == courseId);

            Console.WriteLine($"Enrollments before: {before}");

            // When a course is deleted, all associated enrollments are also deleted due to the cascade delete behavior defined in the database schema
            db.Courses.Remove(course);
            db.SaveChanges();

            int after = db.Enrollments
                .Count(e => e.CourseId == courseId);

            Console.WriteLine($"Enrollments after: {after}");
        }


        static void BulkPriceRaise(appContext db)
        {
            var affectedRows = db.Courses
                .Where(c => c.IsPublished &&
                            c.Level == CourseLevel.Beginner)
                .ExecuteUpdate(s =>
                    s.SetProperty(
                        c => c.Price,
                        c => c.Price * 1.10m));

            Console.WriteLine($"Updated rows: {affectedRows}");
        }

        static void BulkDelete(appContext db)
        {
            var affectedRows = db.Enrollments
                .Where(e => e.Status == EnrollmentStatus.Dropped)
                .ExecuteDelete();

            Console.WriteLine($"Deleted rows: {affectedRows}");
        }
    }
}
