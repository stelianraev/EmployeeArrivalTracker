using EmployeeArrivalTracker.Data.Models;

namespace EmployeeArrivalTracker.Models.EmployeeArrival
{
    public class EmployeeArrivalListingViewModel
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public string Surname { get; init; }
        public string Role { get; init; }
        public string Manager { get; init; }

        //TO DO maybe cript it GDPR
        public string Email { get; init; }
        public DateTime ArrivedTime { get; init; }
    }
}
