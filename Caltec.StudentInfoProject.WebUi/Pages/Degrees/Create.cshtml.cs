using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Caltec.StudentInfoProject.Domain;

namespace Caltec.StudentInfoProject.WebUi.Pages.Degrees
{
    public class CreateModel : PageModel
    {
        private readonly Caltec.StudentInfoProject.Persistence.StudentInfoDbContext _context;

        public CreateModel(Caltec.StudentInfoProject.Persistence.StudentInfoDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Degree Degree { get; set; }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Degrees.Add(Degree);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
