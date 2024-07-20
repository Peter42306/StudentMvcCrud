using Microsoft.EntityFrameworkCore;

namespace StudentMvcCrud.Models
{    
        public class StudentContext : DbContext
        {
            public DbSet<Student> Students { get; set; }

            public StudentContext(DbContextOptions<StudentContext> options) : base(options)
            {
                if (Database.EnsureCreated())
                {
                    Students?.Add(new Student { Name = "John", Surname = "Doe", Age = 21, GPA = 3.5 });
                    Students?.Add(new Student { Name = "Emily", Surname = "Smith", Age = 22, GPA = 3.8 });
                    Students?.Add(new Student { Name = "Michael", Surname = "Johnson", Age = 24, GPA = 3.9 });
                    SaveChanges();
                }
            }
        }    
}
