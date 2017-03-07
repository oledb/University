using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;

namespace University
{
    public class StudentManager
    {
        private IContextFactory _factory;
        public StudentManager(IContextFactory factory)
        {
            _factory = factory;
        }

        public void Add(Student s)
        {
            using (var context = _factory.Create())
            {
                context.Students.Add(s);
                context.SaveChanges();
            }
        }

        public List<Student> Get()
        {
            using (var context = _factory.Create())
            {
                return context.Students.ToList();
            }
        }

        public void Remove(int id)
        {
            using (var context = _factory.Create())
            {
                var temp = new Student() { StudentID = id };
                context.Entry<Student>(temp).State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public void Update(Student student)
        {
            using (var context = _factory.Create())
            {
                context.Entry(student).State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}
