using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeArrivalTracker.Data.Models
{
    /// <summary>
    /// The idea to do class as this is we can extend information about employee erriving as:
    /// card id, car parking if tomorrow this is a option
    /// </summary>
    public class ArrivalInformation
    {
        public int Id { get; init; }
        public DateTime When { get; init; }
        public int EmployeeId {get; init; }
        public Employee Employee { get; init; }
    }
}
