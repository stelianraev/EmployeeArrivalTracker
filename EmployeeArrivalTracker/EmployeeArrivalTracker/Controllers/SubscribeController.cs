namespace EmployeeArrivalTracker.Controllers
{
    using EmployeeArrivalTracker.Models;
    using EmployeeArrivalTracker.Models.EmployeeArrival;
    using EmployeeArrivalTracker.Services;
    using Microsoft.AspNetCore.Mvc;

    [Route("[controller]")]
    public class SubscribeController : Controller
    {
        private readonly ComunicationService _subscriberService;
        private readonly ILogger _logger;
        private static WebServiceToken _fourthToken = new WebServiceToken();
        private static RequestDateModel _steatelessRequestDate;
        public SubscribeController(ComunicationService subscriberService, ILogger<SubscribeController> logger)
        {
            _logger = logger;
            _subscriberService = subscriberService;
        }

        public IActionResult Subscribe() => this.View(new RequestDateModel());

        [HttpPost]
        public async Task<IActionResult> Subscribe([FromForm]RequestDateModel requestModel)
        {
            try
            {
                if (requestModel.Date == null)
                {
                    this.ModelState.AddModelError(nameof(requestModel.Date), "Date is required");
                    return View(requestModel);
                } 

                var responseToken = await _subscriberService.SubscribeToWebService(requestModel);
                _fourthToken.Token = responseToken.Token;
                _fourthToken.When = responseToken.When;

                if(_fourthToken == null)
                {
                    this.ModelState.AddModelError(nameof(_fourthToken), "WebService connection issue...");
                    return View(requestModel);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while trying to subscribe to WebService: {Error}", ex.Message);
            }

            _steatelessRequestDate = requestModel;
            return RedirectToAction("Employees", "EmployeeArrival", _steatelessRequestDate);
            //return View(requestModel);
        }

        [HttpPost, Route("getdata")]
        public IActionResult GetData([FromBody] List<EmployeeArrival> arrivals)
        {
            var token = Request.Headers["X-Fourth-Token"];

            if (string.IsNullOrEmpty(token) || token != _fourthToken.Token)
            {
                return BadRequest("Token is missing or invalid");
                //Response.StatusCode = 401;
                //RedirectToAction(nameof(EmployeArrivalController.Employees));
            }

            _subscriberService.GetDataFromWebService(arrivals);

            return Ok();
            //Response.StatusCode = 200;
        }
            
    }
}


