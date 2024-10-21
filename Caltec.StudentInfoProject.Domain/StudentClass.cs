using Caltec.StudentInfoProject.Domain.Common;

namespace Caltec.StudentInfoProject.Domain
{
    public class StudentClass : EntityBase
    {
        public StudentClass()
        {
            Students = new List<Student>();
        }
        public string? Name { get; set; }
        public string? Description { get; set; }

        public Degree? Degree { get; set; }
        public List<Student> Students { get; set; }
    }
}
