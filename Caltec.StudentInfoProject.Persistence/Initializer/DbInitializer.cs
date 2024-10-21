using Caltec.StudentInfoProject.Domain;

namespace Caltec.StudentInfoProject.Persistence.Initializer
{
    public class DbInitializer
    {

        private static readonly int _nbStudents = 100000;
        private static readonly int _nbDegrees = 20;
        private static readonly int _studentPerClasses = 25;
                
        private static readonly Random random = new Random();

        public static void Initialize(StudentInfoDbContext context)
        {
            context.Database.EnsureCreated();
                       
            var degrees = CreateDegrees(_nbDegrees);
            var students = CreateStudents(_nbStudents);
            var classes = CreateClassesAndFees(_studentPerClasses, students, degrees);

            context.StudentClasses.AddRange(classes);

            context.SaveChanges();
        }

        private static List<Degree> CreateDegrees(int nbDegree)
        {
            List<Degree> degrees = new List<Degree>();


            for (int i = 0; i < nbDegree; i++)
                        {
                degrees.Add(new Degree()
                {
                    Name = MockingData.Degrees[random.Next(0, MockingData.Degrees.Count - 1)] + i.ToString(),
                    NbYear = random.Next(1, 5),
                    FeesPerYearPerStudent = random.Next(1000, 10000),
                    NbPayment = random.Next(1, 12)
                });
            }

            return degrees;
        }
        private static List<Student> CreateStudents(int nbStudent)
        {
            List<Student> students = new List<Student>();

            for (int i = 0; i < nbStudent; i++)
            {
                var FirstName = MockingData.FirstNames[random.Next(0, MockingData.FirstNames.Count - 1)];
                var LastName = MockingData.LastNames[random.Next(0, MockingData.LastNames.Count - 1)];
                var email = $" {FirstName.Replace(" ", string.Empty)}.{LastName.Replace(" ", string.Empty)}@caltech.com";
                students.Add(new Student()
                {
                    FirstName = FirstName,
                    LastName = LastName,
                    Email = email,
                    Phone = MockingData.Phones[random.Next(0, MockingData.Phones.Count - 1)],
                    Address = MockingData.Addresses[random.Next(0, MockingData.Addresses.Count - 1)],
                    City = MockingData.Cities[random.Next(0, MockingData.Cities.Count - 1)],
                    Country = MockingData.Countries[random.Next(0, MockingData.Countries.Count - 1)]
                });
            
            }

            return students;
        }

        private static List<StudentClass> CreateClassesAndFees(int nbStudentPerClass, List<Student> students, List<Degree> degrees)
        {
            List<StudentClass> classes = new List<StudentClass>();

            int countStudentPerClass = 0;
            int ClassCount = 0;
            Degree currentDegree = degrees[random.Next(0, degrees.Count - 1)];
            var currentClass = new StudentClass()
            {
                Degree = degrees[random.Next(0, degrees.Count - 1)],
                Name = $"Class {currentDegree.Name} {ClassCount}",
            };
            foreach (var s in students)
            {
                currentClass.Students.Add(s);
                s.Fees = new List<SchoolFees>();
                for (int i = 1; i < currentDegree.NbPayment; i++)
                {
                    s.Fees.Add(new SchoolFees()
                    {
                        Class = currentClass,
                        Amount = currentDegree.FeesPerYearPerStudent / currentDegree.NbPayment,
                        PaymentDate = DateTime.Now.AddMonths(i),
                        PaymentMethod = MockingData.PaymentMethods[random.Next(0, MockingData.PaymentMethods.Count - 1)],
                        PaymentReference = "Ref" + i.ToString(),
                        PaymentNote = "Note" + i.ToString(),
                        PaymentStatus = MockingData.PaymentStatus[random.Next(0, MockingData.PaymentStatus.Count - 1)]
                    });
                }
                
                if (countStudentPerClass == nbStudentPerClass)
                {
                    classes.Add(currentClass);
                    countStudentPerClass = 0;
                    ClassCount++; 
                    currentClass = new StudentClass()
                    {
                        Degree = degrees[random.Next(0, degrees.Count - 1)],
                        Name = $"Class {currentDegree.Name} {ClassCount}",
                    };
                }

                countStudentPerClass++;
            }

            return classes;
        }
    }
}
