using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University
{
    public class Teacher
    {
        public int TeacherID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public IEnumerable<Subject> Subjects { get; set; }
    }
}
