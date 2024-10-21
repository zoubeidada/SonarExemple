namespace Caltec.StudentInfoProject.Business.Dto
{
    public class StudentDto
    {
        public long Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Zip { get; set; }
        public string? Country { get; set; }
        public string? ClassName { get; set; }
        public long? ClassId { get; set; }
        public decimal SumOfFees { get; set; }
    }
}
