namespace EmployeeArrivalTracker.Services.EmployeeArrival
{
    using EmployeeArrivalTracker.Models.EmployeeArrival;
    public interface IEmployeeDataExtractionService
    {
        EmployeeArrivalQueryModel QueryService(EmployeeArrivalQueryModel query, RequestDateModel requestDateModel);
    }
}
