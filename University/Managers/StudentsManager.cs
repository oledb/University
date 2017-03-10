using System;
using System.Collections.Generic;
using System.Linq;
using LinqKit;
using System.Data.Entity;

namespace University
{
    public class StudentsManager : PocoManager<Student>
    {
        public StudentsManager(IContextFactory factory) : base(factory) { }

        public void DeleteInfo(Student student)
        {
                useContext(context => 
                {
                    context.Entry(student.Info).State = EntityState.Deleted;
                    context.Entry(student).State = EntityState.Modified;
                });
        }

        protected override void createObject(Student obj, UniversityContext context)
        {
            context.Students.Add(obj);
        }

        protected override IEnumerable<Student> readObject(Func<Student, bool> isValid, UniversityContext context)
        {
            return context.Students.Include(s => s.Info).AsExpandable().Where(isValid);
        }

        protected override void updateObject(Student obj, UniversityContext context)
        {
            var old = ReadSingle(s => s.StudentID == obj.StudentID);
            var infoIsNull = old.Info == null;
            if (infoIsNull && obj.Info != null)
            {
                obj.Info.Student = obj;
                context.StudentInfos.Add(obj.Info);
            }
            else if (!infoIsNull && obj.Info != null)
                context.Entry(obj.Info).State = EntityState.Modified;
            context.Entry(obj).State = EntityState.Modified;
            context.SaveChanges();
        }

        protected override void deleteObject(Student obj, UniversityContext context)
        {
            if (obj.Info != null)
                context.Entry(obj.Info).State = EntityState.Deleted;
            context.Entry(obj).State = EntityState.Deleted;
        }
    }
}

