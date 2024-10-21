using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Caltec.StudentInfoProject.Domain;

namespace Caltec.StudentInfoProject.WebUi.Pages.Degrees
{
    public class DeleteModel : PageModel
    {
        private readonly Caltec.StudentInfoProject.Persistence.StudentInfoDbContext _context;

        public DeleteModel(Caltec.StudentInfoProject.Persistence.StudentInfoDbContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Degree Degree { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null || _context.Degrees == null)
            {
                return NotFound();
            }

            var degree = await _context.Degrees.FirstOrDefaultAsync(m => m.Id == id);

            if (degree == null)
            {
                return NotFound();
            }
            else 
            {
                Degree = degree;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(long? id)
        {
            if (id == null || _context.Degrees == null)
            {
                return NotFound();
            }
            var degree = await _context.Degrees.FindAsync(id);

            if (degree != null)
            {
                Degree = degree;
                _context.Degrees.Remove(Degree);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
