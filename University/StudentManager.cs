using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
