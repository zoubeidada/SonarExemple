using Caltec.StudentInfoProject.Domain.Common;

namespace Caltec.StudentInfoProject.Domain
{
    public class Student : EntityBase
    {
        public Student()
        {
            Fees = new List<SchoolFees>();
        }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Zip { get; set; }
        public string? Country { get; set; }

        public StudentClass? Class { get; set; }
        public List<SchoolFees> Fees { get; set; }
    }
}
