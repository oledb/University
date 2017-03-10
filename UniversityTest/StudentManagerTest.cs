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
            var students = new StudentsManager(factory);

            //Act
            students.Create(s);
            List<Student> st = students.Read();

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
            var students = new StudentsManager(factory);

            //Act
            var list = students.Read();

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
            var student = students.ReadSingle(s => s.Name == "Albert");
            students.Delete(student);
            var student2 = students.ReadSingle(s => s.Name == "Albert");
            var list = students.Read();

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
            var students = new StudentsManager(factory);
            var student = students.ReadSingle(s => s.Name == "Albert");

            //Act
            student.Name = "Gregory";
            students.Update(student);
            var student2 = students.ReadSingle(s => s.Name == "Gregory");

            //Assert
            Assert.IsNotNull(student2);
            Assert.AreEqual("Einstein", student2.Surname);
        }

        [Test]
        public void AddStudentInfo()
        {
            //Arrange
            var factory = new FakeContextFactory();
            var students = new StudentsManager(factory);
            var student = new Student()
            {
                Name = "Isaak",
                Surname = "Newton",
                Age = 25,
            };
            students.Create(student);

            //Act
            var info = new StudentInfo()
            {
                Email = "Isaak.Newton@example.com",
                Hobby = "Sience",
                Additional = "Love apples"
            };
            student.Info = info;
            students.Update(student);
            var output = students.ReadSingle(s => s.Name == "Isaak");

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
            var students = new StudentsManager(factory);
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
            students.Create(student);
            var output = students.ReadSingle(s => s.Name == "Isaak");

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
            var students = new StudentsManager(factory);
            students.Create(new Student()
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
            var student = students.ReadSingle(s => s.Name == "Isaak");
            student.Info.Email = "Isaak.Newton@example.com";
            students.Update(student);
            var output = students.ReadSingle(s => s.Name == "Isaak");

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
            var students = new StudentsManager(factory);
            students.Create(new Student()
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
            var student = students.ReadSingle(s => s.Name == "Isaak");
            students.DeleteInfo(student);
            var output = students.ReadSingle(s => s.Name == "Isaak");

            //Assert
            Assert.IsNotNull(output);
            Assert.IsNull(output.Info);
        }
    }
}
