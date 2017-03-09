using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using University;

namespace UniversityTest
{
    [TestFixture]
    public class StudentManagerTest
    {
        [SetUp]
        public void CreateDB()
        {
            FakeContextFactory.RecreateDb();
        }

        [Test]
        public void AddStudentToDB()
        {
            //Arrange
            var s = new Student()
            {
                Name = "Jhon",
                Surname = "Rembo",
                Age = 20
            };
            var factory = new FakeContextFactory();
            var students = new University.StudentsManager(factory);

            //Act
            students.Add(s);
            List<Student> st = students.Get();

            //Assert
            Assert.AreEqual(1, st.Count);
            Assert.AreEqual("Jhon", st[0].Name);
            Assert.AreEqual("Rembo", st[0].Surname);
            Assert.AreEqual(20, st[0].Age);
        }

        [Test]
        public void Create5Sudent()
        {
            //Arrange
            var factory = new FakeContextFactory();
            factory.CreateStudents();
            var students = new University.StudentsManager(factory);

            //Act
            var list = students.Get();

            //Assert
            Assert.AreEqual(5, list.Count);
        }

        [Test]
        public void RemoveStudentFromDB()
        {
            //Arrange
            var factory = new FakeContextFactory();
            factory.CreateStudents();
            var students = new StudentsManager(factory);

            //Act
            var student = students.Get().Where(s => s.Name == "Albert").SingleOrDefault();
            students.Remove(student.StudentID);
            var student2 = students.Get().Where(s => s.Name == "Albert").SingleOrDefault();
            var list = students.Get();

            //Assert
            Assert.IsNull(student2);
            Assert.AreEqual(4, list.Count);
        }

        [Test]
        public void UpdateStudent()
        {
            //Arrange
            var factory = new FakeContextFactory();
            factory.CreateStudents();
            var students = new University.StudentsManager(factory);
            var student = students.Get().Where(s => s.Name == "Albert").SingleOrDefault();

            //Act
            student.Name = "Gregory";
            students.Update(student);
            var student2 = students.Get().Where(s => s.Name == "Gregory").SingleOrDefault();

            //Assert
            Assert.IsNotNull(student2);
            Assert.AreEqual("Einstein", student2.Surname);
        }

        [Test]
        public void AddStudentInfo()
        {
            //Arrange
            var factory = new FakeContextFactory();
            var students = new University.StudentsManager(factory);
            var student = new Student()
            {
                Name = "Isaak",
                Surname = "Newton",
                Age = 25,
            };
            students.Add(student);

            //Act
            var info = new StudentInfo()
            {
                Email = "Isaak.Newton@example.com",
                Hobby = "Sience",
                Additional = "Love apples"
            };
            student.Info = info;
            students.Update(student);
            var output = students.Get().Where(s => s.Name == "Issak").SingleOrDefault();

            //Assert
            Assert.IsNotNull(student.Info);
            Assert.AreEqual("Isaak.Newton@example.com", student.Info.Email);
            Assert.AreEqual("Love apples", student.Info.Additional);
        }

        [Test]
        public void AddStudentWithInfo()
        {
            //Arrange
            var factory = new FakeContextFactory();
            var students = new University.StudentsManager(factory);
            var student = new Student()
            {
                Name = "Isaak",
                Surname = "Newton",
                Age = 25,
                Info = new StudentInfo()
                {
                    Email = "Isaak.Newton@example.com",
                    Hobby = "Sience",
                    Additional = "Love apples"
                }
            };
            //Act
            students.Add(student);
            var output = students.Get().Where(s => s.Name == "Isaak").SingleOrDefault();

            //Assert
            Assert.IsNotNull(output);
            Assert.IsNotNull(output.Info);
            Assert.AreEqual("Isaak", output.Name);
            Assert.AreEqual("Isaak.Newton@example.com", output.Info.Email);
        }

        [Test]
        public void UpdateStudentInfo()
        {
            //Arrange
            var factory = new FakeContextFactory();
            var students = new University.StudentsManager(factory);
            students.Add(new Student()
            {
                Name = "Isaak",
                Surname = "Newton",
                Age = 25,
                Info = new StudentInfo()
                {
                    Email = "Albert.Einstein@example.com",
                    Hobby = "Sience",
                    Additional = "Love apples"
                }
            });
            

            //Act
            var student = students.Get().Where(s => s.Name == "Isaak").SingleOrDefault();
            student.Info.Email = "Isaak.Newton@example.com";
            students.Update(student);
            var output = students.Get().Where(s => s.Name == "Isaak").SingleOrDefault();

            //Assert
            Assert.IsNotNull(output);
            Assert.IsNotNull(output.Info);
            Assert.AreEqual("Isaak", output.Name);
            Assert.AreEqual("Isaak.Newton@example.com", output.Info.Email);
        }

        [Test]
        public void RemoveStudentInfo()
        {
            //Arrange
            var factory = new FakeContextFactory();
            var students = new University.StudentsManager(factory);
            students.Add(new Student()
            {
                Name = "Isaak",
                Surname = "Newton",
                Age = 25,
                Info = new StudentInfo()
                {
                    Email = "Albert.Einstein@example.com",
                    Hobby = "Sience",
                    Additional = "Love apples"
                }
            });

            //Act
            var student = students.Get().Where(s => s.Name == "Isaak").SingleOrDefault();
            students.RemoveInfo(student);
            var output = students.Get().Where(s => s.Name == "Isaak").SingleOrDefault();

            //Assert
            Assert.IsNotNull(output);
            Assert.IsNull(output.Info);
        }
    }
}
