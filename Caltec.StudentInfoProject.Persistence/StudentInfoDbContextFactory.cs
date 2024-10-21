using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

namespace Caltec.StudentInfoProject.Persistence
{
    internal class StudentInfoDbContextFactory: IDesignTimeDbContextFactory<StudentInfoDbContext>
    {
        public StudentInfoDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<StudentInfoDbContext>();
            
            string connexionString = @"";


            //string connexionString = "tcp:trainingmanagementsqlserver.Database.windows.net:1433;Initial Catalog=TrainingManagement;Persist Security Info=False;User ID=trainingadmin;Password=DevDb2019!;MultipleActiveResultsSets=False;Encrypt=True;ConnectionTimeout=30;";
            optionsBuilder.UseSqlServer(connexionString);

            return new StudentInfoDbContext(optionsBuilder.Options);
        }
    }
}
