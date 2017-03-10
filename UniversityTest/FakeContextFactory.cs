using Effort;
using System.Data.Common;
using University;
using System.Diagnostics;

namespace UniversityTest
{
    public class FakeContextFactory : IContextFactory
    {
        static private DbConnection connection;
        static FakeContextFactory()
        {
            RecreateDb();

        }
        public UniversityContext Create()
        {
            var context = new UniversityContext(connection);
            context.Database.CreateIfNotExists();
            context.Database.Log = Write;
            return context;
        }

        static void Write(string value)
        {
            Debug.Write(value,"SQL Output");
        }

        public static void RecreateDb()
        {
            connection = DbConnectionFactory.CreateTransient();
        }

        public void CreateStudents()
        {
            using (var context = Create())
            {
                context.Students.Add(new Student()
                {
                    Name = "Jhon",
                    Surname = "Watson",
                    Age = 21
                });
                context.Students.Add(new Student()
                {
                    Name = "Rebbeka",
                    Surname = "Brown",
                    Age = 20
                });
                context.Students.Add(new Student()
                {
                    Name = "Marie",
                    Surname = "Curie",
                    Age = 19
                });
                context.Students.Add(new Student()
                {
                    Name = "Richard",
                    Surname = "The Lionheart",
                    Age = 21
                });
                context.Students.Add(new Student()
                {
                    Name = "Albert",
                    Surname = "Einstein",
                    Age = 23
                });
                context.SaveChanges();
            }
        }
    }
}
