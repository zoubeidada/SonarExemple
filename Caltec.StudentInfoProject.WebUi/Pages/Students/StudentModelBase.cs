using Caltec.StudentInfoProject.Business;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Caltec.StudentInfoProject.WebUi.Pages.Students
{
    public class StudentModelBase : PageModel
    {
        protected readonly StudentService _service;

        public StudentModelBase(StudentService service)
        {
            _service = service;
        }
    }
}
