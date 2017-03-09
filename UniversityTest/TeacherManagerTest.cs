using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using University;

namespace UniversityTest
{
    [TestFixture]
    class TeacherManagerTest
    {
        [SetUp]
        public void CreateDB()
        {
            FakeContextFactory.RecreateDb();
        }

        [Test]
        public void AddTeacherToDb()
        {
            //Arrange
            var factory = new FakeContextFactory();
            var teacher = new Teacher()
            {
                Name = "Karl",
                Surname = "Marx"
            };
            var teachers = new TeacherManager(factory);

            //Act
            teachers.Add(teacher);
            var output = teachers.Get();

            //Assert
            Assert.AreEqual(1, output.Count);
            Assert.AreEqual("Marx", output[0].Surname);
        }
    }
}
