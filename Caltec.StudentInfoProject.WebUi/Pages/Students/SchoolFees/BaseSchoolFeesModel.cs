using Caltec.StudentInfoProject.Business;
using Caltec.StudentInfoProject.Business.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Caltec.StudentInfoProject.WebUi.Pages.Students.SchoolFees
{
    public class BaseSchoolFeesModel : PageModel
    {
        protected readonly StudentService _service;
        protected readonly SchoolFeesService _schoolFeesService;
        protected readonly DegreeService _degreeService;
        protected readonly StudentClassService _studentClassService;

        public BaseSchoolFeesModel(StudentService service, SchoolFeesService schoolFeesService, DegreeService degreeService, StudentClassService studentClassService)
        {
            _service = service;
            _schoolFeesService = schoolFeesService;
            _degreeService = degreeService;
            _studentClassService = studentClassService;
        }

        
        
        [BindProperty]
        public StudentDto Student { get; set; } = default!;
        [BindProperty]
        public List<SelectListItem> StudentClasses { get; set; } = default!;
        [BindProperty]
        public SchoolFeesDto SchoolFees { get; set; } = default!;

        public List<string> PaymentStatusList { get; set; } = new List<string>()
        {
            "Paid",
            "Unpaid",
            "Partially Paid"
        };

        public List<string> PaymentMethodsList { get; set; } = new List<string>()
        {
            "Cash",
            "Check",
            "Credit Card",
            "Debit Card",
            "Paypal",
            "Bitcoin"
        };

        public async Task LoadDataForPage(long? studentId, long? currentFeeId)
        {
            if (currentFeeId.HasValue)
            {
                SchoolFees = await _schoolFeesService.GetOneAsync(currentFeeId.Value, CancellationToken.None);
                Student = await _service.GetOne(SchoolFees.StudentId, CancellationToken.None);
            }
            else
            {
                Student = await _service.GetOne(studentId.Value, CancellationToken.None);
            }

            var studentClassesDto = await _studentClassService.GetAllStudentClassesAsync(CancellationToken.None);
            StudentClasses = studentClassesDto.Select(x => new SelectListItem(x.Name, x.Id.ToString())).ToList();
        }
    }
}
