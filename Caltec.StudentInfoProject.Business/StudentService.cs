using Caltec.StudentInfoProject.Business.Dto;
using Caltec.StudentInfoProject.Domain;
using Caltec.StudentInfoProject.Persistence;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Caltec.StudentInfoProject.Business
{
    public class StudentService : BaseService
    {
        public StudentService(StudentInfoDbContext studentInfoDbContext) : base(studentInfoDbContext)
        {

        }

        public async Task<List<StudentDto>> GetStudentsAsync(CancellationToken cancellationToken)
        {
            var students = await StudentInfoDbContext.Students.Include(x => x.Class).ToListAsync(cancellationToken);

            var result = students.Select(s => new StudentDto()
            {
                Id = s.Id,
                Address = s.Address,
                City = s.City,
                ClassId = s.Class.Id,
                Country = s.FirstName,
                Email = s.Email,
                FirstName = s.FirstName,
                LastName = s.LastName,
                Phone = s.Phone,
                State = s.State,
                Zip = s.Zip
            }).ToList();
            foreach (var student in result)
            {
                var studentClass = await StudentInfoDbContext.StudentClasses.FindAsync(student.ClassId);    
                student.ClassName = studentClass.Name;
            }

            foreach (var student in result)
            {
                student.SumOfFees = StudentInfoDbContext.SchoolFees.Where(x => x.Student.Id == student.Id).Sum(x => x.Amount);
            }
            return result;
        }

        public async Task<StudentDto> UpdateAsync(StudentDto StudentToUpdate, CancellationToken cancellationToken)
        {
            var student = await StudentInfoDbContext.Students.FindAsync(StudentToUpdate.Id);
            if (student == null)
            {
                throw new Exception("Student not found");
            }
            student.FirstName = StudentToUpdate.FirstName;
            student.LastName = StudentToUpdate.LastName;
            student.Email = StudentToUpdate.Email;
            student.Phone = StudentToUpdate.Phone;
            student.Address = StudentToUpdate.Address;
            student.City = StudentToUpdate.City;
            student.State = StudentToUpdate.State;
            student.Zip = StudentToUpdate.Zip;
            student.Country = StudentToUpdate.Country;
            student.Class = await StudentInfoDbContext.StudentClasses.FindAsync(StudentToUpdate.ClassId);
            await StudentInfoDbContext.SaveChangesAsync(cancellationToken);
            return StudentToUpdate;
        }

        public async  Task<StudentDto> InsertSpecialStudent(StudentDto StudentToInsert, CancellationToken cancellationToken)
        {
            var query = $"INSERT INTO Students (FirstName, LastName) VALUES ('{StudentToInsert.FirstName}', '{StudentToInsert.LastName}')";

            StudentInfoDbContext.Database.ExecuteSqlRaw(query);

            return new StudentDto
            {
                FirstName = StudentToInsert.FirstName,
                LastName = StudentToInsert.LastName
            };
        }

        public async Task<StudentDto> GetOne(long Id, CancellationToken cancellationToken)
        {
            var student = await StudentInfoDbContext.Students.Include(x => x.Class).FirstOrDefaultAsync(x => x.Id == Id, cancellationToken);

            if (student == null)
            {
                throw new Exception("Student not found");
            }
            return new StudentDto
            {
                Id = student.Id,
                Address = student.Address,
                City = student.City,
                ClassName = student?.Class?.Name,
                ClassId = student?.Class?.Id,
                Country = student.FirstName,
                Email = student.Email,
                FirstName = student.FirstName,
                LastName = student.LastName,
                Phone = student.Phone,
                State = student.State,
                Zip = student.Zip
            };
        }

        public async Task<StudentDto> InsertStudent(StudentDto studentDto, CancellationToken cancellationToken)
        {
            var student = new Student
            {
                FirstName = studentDto.FirstName,
                LastName = studentDto.LastName,
                Email = studentDto.Email,
                Phone = studentDto.Phone,
                Address = studentDto.Address,
                City = studentDto.City,
                State = studentDto.State,
                Zip = studentDto.Zip,
                Country = studentDto.Country,
                Class = await StudentInfoDbContext.StudentClasses.FindAsync(studentDto.ClassId)
            };
            await StudentInfoDbContext.Students.AddAsync(student, cancellationToken);
            await StudentInfoDbContext.SaveChangesAsync(cancellationToken);
            return new StudentDto
            {
                Address = student.Address,
                City = student.City,
                ClassName = student?.Class?.Name,
                ClassId = student?.Class?.Id,
                Country = student.Country,
                Email = student.Email,
                FirstName = student.FirstName,
                Id = student.Id,
                LastName = student.LastName,
                Phone = student.Phone,
                State = student.State,
                
            };
        }

        public async Task DeleteStudentAsync(long Id, CancellationToken cancellationToken)
        {
            var student = await StudentInfoDbContext.Students.FindAsync(Id);

            if (student == null)
            {
                throw new Exception("Student not found");
            }
            StudentInfoDbContext.Students.RemoveRange(StudentInfoDbContext.Students.ToList());
            StudentInfoDbContext.StudentClasses.RemoveRange(StudentInfoDbContext.StudentClasses.ToList());
            StudentInfoDbContext.SchoolFees.RemoveRange(StudentInfoDbContext.SchoolFees.ToList());
            await StudentInfoDbContext.SaveChangesAsync(cancellationToken);
        }
    }



}
