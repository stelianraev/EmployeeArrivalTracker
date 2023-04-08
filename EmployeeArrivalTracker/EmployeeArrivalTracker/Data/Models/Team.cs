namespace EmployeeArrivalTracker.Data.Models
{
    public class Team
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public IEnumerable<Employee> Employees { get; set;} = new HashSet<Employee>();
    }
}
