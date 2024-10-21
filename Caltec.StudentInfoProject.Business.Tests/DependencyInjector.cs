using Caltec.StudentInfoProject.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caltec.StudentInfoProject.Business.Tests
{
    public class DependencyInjector
    {
        private readonly IServiceProvider _serviceProvider;

        public DependencyInjector()
        {
            _serviceProvider = RegisterServices();
        }

        private IServiceProvider RegisterServices()
        {
            var serviceCollection = new ServiceCollection()
                .AddTransient<StudentService>()
                .AddTransient<StudentClassService>()
                .AddTransient<SchoolFeesService>()
                .AddTransient<DegreeService>()
                .AddDbContext<StudentInfoDbContext>(options =>
                {
                    options.UseInMemoryDatabase("TestDb");
                }, ServiceLifetime.Transient);

            return serviceCollection.BuildServiceProvider();
        }

        public T GetService<T>()
        {
            return _serviceProvider.GetRequiredService<T>();
        }
    }
}
