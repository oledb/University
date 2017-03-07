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
    public class TestUsers
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
            var students = new StudentManager(factory);

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
            var students = new StudentManager(factory);

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
            var students = new StudentManager(factory);

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
        public void UpdateStudentInDb()
        {
            //Arrange
            var factory = new FakeContextFactory();
            factory.CreateStudents();
            var students = new StudentManager(factory);
            var student = students.Get().Where(s => s.Name == "Albert").SingleOrDefault();

            //Act
            student.Name = "Gregory";
            students.Update(student);
            var student2 = students.Get().Where(s => s.Name == "Gregory").SingleOrDefault();

            //Assert
            Assert.IsNotNull(student2);
            Assert.AreEqual("Einstein", student2.Surname);
        }
    }
}
