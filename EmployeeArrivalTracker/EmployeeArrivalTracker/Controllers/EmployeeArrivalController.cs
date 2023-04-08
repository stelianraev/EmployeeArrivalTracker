namespace EmployeeArrivalTracker.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using EmployeeArrivalTracker.Models.EmployeeArrival;
    using EmployeeArrivalTracker.Services.EmployeeArrival;

    [Route("[controller]")]
    public class EmployeeArrivalController : Controller
    {
        private readonly IEmployeeDataExtractionService _employeeDataExtractor;

        public EmployeeArrivalController(IEmployeeDataExtractionService employeeDataExtractor, ILogger<EmployeeArrivalController> logger)
        {
            _employeeDataExtractor = employeeDataExtractor;
        }
        public IActionResult Employees([FromQuery] EmployeeArrivalQueryModel query, RequestDateModel requestDateModel)
        {
            query = _employeeDataExtractor.QueryService(query, requestDateModel);

            return View(query);
        }
    }
}
