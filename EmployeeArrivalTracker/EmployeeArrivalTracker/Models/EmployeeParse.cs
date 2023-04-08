namespace EmployeeArrivalTracker.Models
{
    public class EmployeeParse
    {
        public int Id { get; init; }
        public int? ManagerId { get; init; }
        public int Age { get; init; }
        public string Role { get; init; }       
        public string Email { get; init; }       
        public string Name { get; init; }        
        public string SurName { get; init; }
        public string[] Teams { get; set; }
    }
}
