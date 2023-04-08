namespace EmployeeArrivalTracker.Services
{
    using EmployeeArrivalTracker.Data;
    using EmployeeArrivalTracker.Data.Models;
    using EmployeeArrivalTracker.Models;
    using Microsoft.Extensions.Options;
    using Newtonsoft.Json;
    using EmployeeArrivalTracker.Configuration;

    using EmployeeArrivalModel = EmployeeArrivalTracker.Models.EmployeeArrival;

    public class ComunicationService : ICommunicationService
    {
        private readonly ILogger _logger;
        private EmployeeArrivalTrackerDbContext _dbContext;
        private readonly ServiceConfiguration _configuration;
        public ComunicationService(EmployeeArrivalTrackerDbContext dbContext, IOptions<ServiceConfiguration> configuration, ILogger<ComunicationService> logger)
        {
            _logger = logger;
            _configuration = configuration.Value;
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<WebServiceToken> SubscribeToWebService(EmployeeArrivalModel.RequestDateModel requestModel)
        {
            //TODO move this in config
            //string callBackUri = "https://localhost:7238/Subscribe/getdata";
            string callBackUri = _configuration.CallBackUrl;
            var url = $"http://localhost:51396/api/clients/subscribe?date={requestModel.Date.ToString("yyyy-MM-dd")}&callback={callBackUri}";

            //TODO if null modelstate error here
            WebServiceToken result = null;
            try
            {
                using (var client = new HttpClient())
                {
                    var request = new HttpRequestMessage
                    {
                        Method = HttpMethod.Get,
                        RequestUri = new Uri(url)
                    };

                    request.Headers.Add("Accept-Client", "Fourth-Monitor");

                    using (var response = await client.SendAsync(request))
                    {

                        response.EnsureSuccessStatusCode();
                        var requestResult = await response.Content.ReadAsStringAsync();
                        result = JsonConvert.DeserializeObject<WebServiceToken>(requestResult);
                    }
                }                
            }            
            catch (Exception ex)
            {
                _logger.LogError(ex, "Request problem {RequestProblem}");
            }

            return result;
        }

        public void GetDataFromWebService(List<EmployeeArrivalModel.EmployeeArrival> arrivals)
        {
            //TODO try catch
            // Save the employee arrival information to the database
            var tempArrivalCollection = new List<ArrivalInformation>();

            try
            {
                foreach (var arrival in arrivals)
                {
                    ArrivalInformation tempEmparrival = new ArrivalInformation
                    {
                        EmployeeId = arrival.EmployeeId,
                        When = arrival.When,
                    };

                    tempArrivalCollection.Add(tempEmparrival);
                }

                _dbContext.ArrivalInformation.AddRange(tempArrivalCollection);

                //TODO conflict merging db
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,"Savin data from WebService to DB error {SavingError}");
            }
        }
    }
}

