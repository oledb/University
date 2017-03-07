using System.ComponentModel.DataAnnotations.Schema;

namespace University
{
    public class Student
    {
        public int StudentID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public StudentInfo Info { get; set; }
    }
}
