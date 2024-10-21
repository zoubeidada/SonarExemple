using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caltec.StudentInfoProject.Business.Tests
{
    public abstract class TestBase
    {

        protected DependencyInjector _injector;
        public TestBase()
        {
            _injector = new DependencyInjector();
        }


    }
}