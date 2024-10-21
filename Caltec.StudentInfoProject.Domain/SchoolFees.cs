using Caltec.StudentInfoProject.Domain.Common;

namespace Caltec.StudentInfoProject.Domain
{
    public class SchoolFees : EntityBase
    {
        public Student? Student { get; set; }
        public StudentClass? Class { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string? PaymentMethod { get; set; }
        public string? PaymentReference { get; set; }
        public string? PaymentStatus { get; set; }
        public string? PaymentNote { get; set; }
        
        
    }
    
}
