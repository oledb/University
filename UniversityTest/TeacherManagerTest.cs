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
            teachers.Create(teacher);
            var output = teachers.Read();

            //Assert
            Assert.AreEqual(1, output.Count);
            Assert.AreEqual("Marx", output[0].Surname);
        }

        [Test]
        public void RemoveTeacherToDb()
        {
            //Arrange
            var factory = new FakeContextFactory();
            var teachers = new TeacherManager(factory);
            teachers.Create(new Teacher()
            {
                Name = "Karl",
                Surname = "Marx"
            });
            var teacher = teachers.ReadSingle(t => t.Name == "Karl");

            //Act
            teachers.Delete(teacher);
            var output = teachers.ReadSingle(t => t.Name == "Karl");

            //Assert
            Assert.IsNull(output);
        }

        [Test]
        public void UpdateTeacherToDb()
        {
            //Arrange
            var factory = new FakeContextFactory();
            var teachers = new TeacherManager(factory);
            teachers.Create(new Teacher()
            {
                Name = "Karl",
                Surname = "Marks"
            });
            var teacher = teachers.ReadSingle(t => t.Name == "Karl");

            //Act
            teacher.Surname = "Marx";
            teachers.Update(teacher);
            var output = teachers.ReadSingle(t => t.Name == "Karl");

            //Assert
            Assert.IsNotNull(output);
            Assert.AreEqual("Marx", teacher.Surname);
        }
    }
}
