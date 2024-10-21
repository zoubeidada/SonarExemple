using Microsoft.AspNetCore.Mvc;
using Caltec.StudentInfoProject.Business;

namespace Caltec.StudentInfoProject.WebUi.Pages.Students.SchoolFees
{
    public class DeleteModel : BaseSchoolFeesModel
    {

        public DeleteModel(StudentService service, SchoolFeesService schoolFeesService, DegreeService degreeService, StudentClassService studentClassService) : base(service, schoolFeesService, degreeService, studentClassService)
        {
        }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            await LoadDataForPage(null, id);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            await _schoolFeesService.DeleteOneAsync(id.Value, CancellationToken.None);
            return RedirectToPage("/Students/Details", new { id = Student.Id });
        }
    }
}
