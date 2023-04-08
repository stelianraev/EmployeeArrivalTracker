namespace EmployeeArrivalTracker.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    public class Employee
    {
        public int Id { get; init; }

        public int EmployeeId { get; init; }

        public int? ManagerId { get; init; }

        public int Age { get; init; }       

        public string Role { get; init; }

        [Required]
        [EmailAddress]
        public string Email { get; init; }

        [Required]
        [MaxLength(DataConstants.NameMaxLength)]
        public string Name { get; init; }

        [Required]
        [MaxLength(DataConstants.SurNameNameMaxLenght)]
        public string SurName { get; init; }

        public ICollection<Team> Teams { get; set; } = new HashSet<Team>();

        public List<ArrivalInformation> ArrivalInformation { get; set; } = new List<ArrivalInformation>();
    }
}
