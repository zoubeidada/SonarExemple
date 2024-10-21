using Caltec.StudentInfoProject.Business.Dto;
using Caltec.StudentInfoProject.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Caltec.StudentInfoProject.Business
{
    public class StudentClassService : BaseService
    {
        public StudentClassService(StudentInfoDbContext studentInfoDbContext) : base(studentInfoDbContext)
        {
        }

        public Task<List<StudenClassDto>> GetAllStudentClassesAsync(CancellationToken cancellationToken)
        {
            return StudentInfoDbContext.StudentClasses.Select(s => new StudenClassDto
            {
                Id = s.Id,
                Name = s.Name,
                Degree = s.Degree.Name,
                Description = s.Description

            }).ToListAsync(cancellationToken);
        }
    }
    
}
