using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Caltec.StudentInfoProject.Business.Dto;
using Caltec.StudentInfoProject.Business;

namespace Caltec.StudentInfoProject.WebUi.Pages.Students
{
    public class CreateModel : StudentModelBase
    {
        private readonly StudentClassService _studentClassService;
        public CreateModel(StudentService service, StudentClassService studentClassService) : base(service)
        {
            _studentClassService = studentClassService;
        }
        
        public async Task<IActionResult> OnGetAsync()
        {
            var studentClassesDto = await _studentClassService.GetAllStudentClassesAsync(CancellationToken.None);
            StudentClasses = studentClassesDto.Select(x => new SelectListItem(x.Name, x.Id.ToString())).ToList();
            return Page();
        }

        [BindProperty]
        public StudentDto Student { get; set; }
        [BindProperty]
        public List<SelectListItem> StudentClasses { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid)
            {
                return Page();
            }
            await _service.InsertStudent(Student, cancellationToken: CancellationToken.None);
            return RedirectToPage("./Index");
        }
    }
}
