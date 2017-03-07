using System.Data.Entity;
using System.Data.Common;

namespace University
{
    public class UniversityContext : DbContext
    {
        public UniversityContext() { }
        public UniversityContext(DbConnection connection ) : base(connection, true) { }

        public DbSet<Student> Students { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<StudentInfo> StudentInfos { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
    }
}
