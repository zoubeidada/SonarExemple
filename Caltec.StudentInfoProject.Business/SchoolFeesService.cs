using Caltec.StudentInfoProject.Business.Dto;
using Caltec.StudentInfoProject.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Caltec.StudentInfoProject.Business
{
    public class SchoolFeesService : BaseService
    {
        public SchoolFeesService(StudentInfoDbContext studentInfoDbContext) : base(studentInfoDbContext)
        {
            
        }
        public async Task<List<SchoolFeesDto>> GetSchoolFeesDtosByStudentIdAsync(long studentId, CancellationToken cancellationToken)
        {
            return await StudentInfoDbContext.SchoolFees.Where(s => s.Student.Id == studentId).Select(s => new SchoolFeesDto()
            {
                Id = s.Id,
                Amount = s.Amount,
                PaymentDate = s.PaymentDate,
                PaymentMethod = s.PaymentMethod,
                PaymentNote = s.PaymentNote,
                PaymentReference = s.PaymentReference,
                PaymentStatus = s.PaymentStatus,
                ClassId = s.Class.Id,
                ClassName = s.Class.Name,
                StudentId = s.Student.Id,
                StudentName = s.Student.FirstName + " " + s.Student.LastName

            }).ToListAsync(cancellationToken);
        }
        public async Task<SchoolFeesDto> GetOneAsync(long schoolFeeId, CancellationToken cancellationToken)
        {
            var schoolFee = await StudentInfoDbContext.SchoolFees.Include(x=> x.Student).Include(x=> x.Class).FirstOrDefaultAsync(x=> x.Id == schoolFeeId, cancellationToken);
            if (schoolFee == null)
            {
                throw new Exception("School Fee not found");
            }
            return new SchoolFeesDto()
            {
                Id = schoolFee.Id,
                Amount = schoolFee.Amount,
                PaymentDate = schoolFee.PaymentDate,
                PaymentMethod = schoolFee.PaymentMethod,
                PaymentNote = schoolFee.PaymentNote,
                PaymentReference = schoolFee.PaymentReference,
                PaymentStatus = schoolFee.PaymentStatus,
                ClassId = schoolFee.Class.Id,
                ClassName = schoolFee.Class.Name,
                StudentId = schoolFee.Student.Id,
                StudentName = schoolFee.Student.FirstName + " " + schoolFee.Student.LastName
            };
        }

        public async Task AddAsync(SchoolFeesDto schoolFeesDto, CancellationToken cancellationToken)
        {
            var schoolFee = new Domain.SchoolFees()
            {
                Amount = schoolFeesDto.Amount,
                PaymentDate = schoolFeesDto.PaymentDate,
                PaymentMethod = schoolFeesDto.PaymentMethod,
                PaymentNote = schoolFeesDto.PaymentNote,
                PaymentReference = schoolFeesDto.PaymentReference,
                PaymentStatus = schoolFeesDto.PaymentStatus,
                Class = await StudentInfoDbContext.StudentClasses.FindAsync(schoolFeesDto.ClassId),
                Student = await StudentInfoDbContext.Students.FindAsync(schoolFeesDto.StudentId)
            };
            await StudentInfoDbContext.SchoolFees.AddAsync(schoolFee, cancellationToken);
            await StudentInfoDbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(SchoolFeesDto schoolFeesDto, CancellationToken cancellationToken)
        {
            var schoolFee = await StudentInfoDbContext.SchoolFees.Include(x => x.Student).Include(x => x.Class).FirstOrDefaultAsync(x => x.Id == schoolFeesDto.Id, cancellationToken);
            if (schoolFee == null)
            {
                throw new Exception("School Fee not found");
            }
            schoolFee.Amount = schoolFeesDto.Amount;
            schoolFee.PaymentDate = schoolFeesDto.PaymentDate;
            schoolFee.PaymentMethod = schoolFeesDto.PaymentMethod;
            schoolFee.PaymentNote = schoolFeesDto.PaymentNote;
            schoolFee.PaymentReference = schoolFeesDto.PaymentReference;
            schoolFee.PaymentStatus = schoolFeesDto.PaymentStatus;
            schoolFee.Class = await StudentInfoDbContext.StudentClasses.FindAsync(schoolFeesDto.ClassId);
            schoolFee.Student = await StudentInfoDbContext.Students.FindAsync(schoolFeesDto.StudentId);
            await StudentInfoDbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteOneAsync(long schoolFeeId, CancellationToken cancellationToken)
        {
            var schoolFee = await StudentInfoDbContext.SchoolFees.FirstAsync(x=> x.Id == schoolFeeId, cancellationToken);
            if (schoolFee == null)
            {
                throw new Exception("School Fee not found");
            }
            StudentInfoDbContext.SchoolFees.Remove(schoolFee);
            await StudentInfoDbContext.SaveChangesAsync(cancellationToken);
        }

    }
}
