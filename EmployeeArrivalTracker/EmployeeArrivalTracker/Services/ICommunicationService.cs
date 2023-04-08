namespace EmployeeArrivalTracker.Services
{
    using EmployeeArrivalTracker.Models;    
    using EmployeeArrivalModel = EmployeeArrivalTracker.Models.EmployeeArrival;
    public interface ICommunicationService
    {
        Task<WebServiceToken> SubscribeToWebService(EmployeeArrivalModel.RequestDateModel requestModel);


        void GetDataFromWebService(List<EmployeeArrivalModel.EmployeeArrival> arrivals);
        
    }
}
