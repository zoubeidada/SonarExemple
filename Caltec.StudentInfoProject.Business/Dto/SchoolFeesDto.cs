namespace Caltec.StudentInfoProject.Business.Dto
{
    public class SchoolFeesDto
    {
        public long Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string? PaymentMethod { get; set; }
        public string? PaymentReference { get; set; }
        public string? PaymentStatus { get; set; }
        public string? PaymentNote { get; set; }
        public string? ClassName { get; set; }
        public long ClassId { get; set; }
        public long StudentId { get; set; }
        public string? StudentName { get; set; }
    }
}
