using Caltec.StudentInfoProject.Business;
using Caltec.StudentInfoProject.Business.Dto;

namespace Caltec.StudentInfoProject.WebUi.Pages.Students
{
    public class StudentListModel : StudentModelBase
    {
        public StudentListModel(StudentService service) : base(service)
        {
            
        }

        public IList<StudentDto> Students { get;set; } = default!;

        public async Task OnGetAsync()
        {

            Students = await _service.GetStudentsAsync(CancellationToken.None);

        }
    }
}
