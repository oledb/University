using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University
{
    public class Subject
    {
        public int SubjectID { get; set; }
        public string Name { get; set; }
        public int CountHourInWeek { get; set; }
        public IEnumerable<Student> Students { get; set; }
        public Teacher Teacher { get; set; }
    }
}
