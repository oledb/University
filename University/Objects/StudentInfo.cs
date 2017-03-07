using System.ComponentModel.DataAnnotations.Schema;

namespace University
{
    public class StudentInfo
    {
        [ForeignKey("Student")]
        public int StudentInfoID { get; set; }
        public Student Student { get; set; }
        public string Email { get; set; }
        public string Hobby { get; set; }
        public string Additional { get; set; }
    }
}
