using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University
{
    public class TeacherManager
    {
        private IContextFactory _factory;
        public TeacherManager(IContextFactory factory)
        {
            _factory = factory;
        }
        public void Add(Teacher teacher)
        {
            using (var context = _factory.Create())
            {
                context.Teachers.Add(teacher);
                context.SaveChanges();
            }
        }

        public List<Teacher> Get()
        {
            using (var context = _factory.Create())
            {
                return context.Teachers.ToList();
            }
        }
    }
}
