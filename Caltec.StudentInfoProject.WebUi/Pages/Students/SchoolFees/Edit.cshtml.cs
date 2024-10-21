using Microsoft.AspNetCore.Mvc;
using Caltec.StudentInfoProject.Business;

namespace Caltec.StudentInfoProject.WebUi.Pages.Students.SchoolFees
{
    public class EditModel : BaseSchoolFeesModel
    {
        public EditModel(StudentService service, SchoolFeesService schoolFeesService, DegreeService degreeService, StudentClassService studentClassService) : base(service, schoolFeesService, degreeService, studentClassService)
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

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            SchoolFees.StudentId = Student.Id;
            await _schoolFeesService.UpdateAsync(SchoolFees, cancellationToken: CancellationToken.None);

            return RedirectToPage("/Students/Details", new { id = Student.Id });
        }
    }
}
