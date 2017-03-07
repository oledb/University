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
        }
    }
}
