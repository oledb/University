using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using LinqKit;

namespace University
{
    public class TeacherManager : PocoManager<Teacher>
    {
        public TeacherManager(IContextFactory factory) : base(factory) { }

        protected override void createObject(Teacher obj, UniversityContext context)
        {
            context.Teachers.Add(obj);
        }

        protected override IEnumerable<Teacher> readObject(Func<Teacher, bool> isValid,
            UniversityContext context)
        {
            var query = context.Teachers.AsExpandable().Where(isValid);
            return query;
        }

        protected override void deleteObject(Teacher obj, UniversityContext context)
        {
            context.Entry(obj).State = EntityState.Deleted;
        }

        protected override void updateObject(Teacher obj, UniversityContext context)
        {
            context.Entry(obj).State = EntityState.Modified;
        }
    }
}
