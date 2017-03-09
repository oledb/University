using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;

namespace University
{
    public class StudentsManager
    {
        private IContextFactory _factory;
        public StudentsManager(IContextFactory factory)
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
                return context.Students.Include(s => s.Info).ToList();
        }

        public void Remove(int id)
        {
            using (var context = _factory.Create())
            {
                var temp = new Student() { StudentID = id };
                context.Entry(temp).State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public void Update(Student student)
        {
            var old = Get().Where(s => s.StudentID == student.StudentID).SingleOrDefault();
            var infoIsNull = old.Info == null;
            using (var context = _factory.Create())
            {
                if (infoIsNull && student.Info != null)
                {
                    student.Info.Student = student;
                    context.StudentInfos.Add(student.Info);
                }
                else if (!infoIsNull && student.Info != null)
                    context.Entry(student.Info).State = EntityState.Modified;
                context.Entry(student).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void RemoveInfo(Student student)
        {
            using (var context = _factory.Create())
            {
                context.Entry(student.Info).State = EntityState.Deleted;
                context.Entry(student).State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}

