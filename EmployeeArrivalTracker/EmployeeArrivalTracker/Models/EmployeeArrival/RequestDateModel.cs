namespace EmployeeArrivalTracker.Models.EmployeeArrival
{
    using System.ComponentModel.DataAnnotations;
    public class RequestDateModel
    {
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required]
        public DateTime Date { get; set; }
    }
}
