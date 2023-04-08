namespace EmployeeArrivalTracker.Models.EmployeeArrival
{
    using InfluencerWannaBe.Models;
    using EmployeeArrivalTracker.Configuration;
    using EmployeeArrivalTracker.Models.EmployeeArrival.Enums;
    using Microsoft.Extensions.Options;

    public class EmployeeArrivalQueryModel : PageSettingsAbstract<EmployeeArrivalSorting, EmployeeArrivalListingViewModel>
    {
        public const int EmployeePerPage = 50;

        public DateTime Date { get; set; }
    }
}
