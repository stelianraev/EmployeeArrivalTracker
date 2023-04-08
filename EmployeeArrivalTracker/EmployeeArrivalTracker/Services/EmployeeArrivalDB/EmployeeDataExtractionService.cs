namespace EmployeeArrivalTracker.Services.EmployeeArrival
{
    using EmployeeArrivalTracker.Data;
    using EmployeeArrivalTracker.Models.EmployeeArrival;
    using EmployeeArrivalTracker.Models.EmployeeArrival.Enums;
    using Microsoft.IdentityModel.Protocols;

    public class EmployeeDataExtractionService : IEmployeeDataExtractionService
    {
        private readonly EmployeeArrivalTrackerDbContext _context;
        private readonly ILogger _logger;
        public EmployeeDataExtractionService(EmployeeArrivalTrackerDbContext context, ILogger<EmployeeDataExtractionService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public EmployeeArrivalQueryModel QueryService(EmployeeArrivalQueryModel query, RequestDateModel requestDateModel)
        {
            try
            {
                var employeeQuery = _context.ArrivalInformation.Where(x => x.When.Date == requestDateModel.Date.Date).AsQueryable();

                query.Date = requestDateModel.Date;
                if (!string.IsNullOrWhiteSpace(query.SearchTerm))
                {
                    //TO DO add more search terms
                    employeeQuery = employeeQuery.Where(e =>
                   (e.Employee.Name).ToLower().Contains(query.SearchTerm.ToLower()) ||
                    e.Employee.Role.ToLower().Contains(query.SearchTerm.ToLower()));
                }

                employeeQuery = query.Sorting switch
                {
                    EmployeeArrivalSorting.Name => employeeQuery.OrderBy(e => e.Employee.Name),
                    EmployeeArrivalSorting.Role => employeeQuery.OrderBy(e => e.Employee.Role),
                    EmployeeArrivalSorting.EmployeeId => employeeQuery.OrderBy(e => e.Id),
                    EmployeeArrivalSorting.ArrivalTime => employeeQuery.OrderBy(e => e.When)
                };

                var totalEmployees = employeeQuery.Count();

                var employees = employeeQuery
                    .Skip((query.CurrentPage - 1) * EmployeeArrivalQueryModel.EmployeePerPage)
                    .Take(EmployeeArrivalQueryModel.EmployeePerPage)
                    .Select(e => new EmployeeArrivalListingViewModel
                    {
                        Name = e.Employee.Name,
                        ArrivedTime = e.When.Date,
                        Email = e.Employee.Email,
                        Id = e.Id,
                        Manager = _context.Employees.FirstOrDefault(x => x.EmployeeId == e.Employee.ManagerId).Name,
                        Role = e.Employee.Role,
                        Surname = e.Employee.SurName
                    })
                    .ToList();

                query.TotalElements = totalEmployees;
                query.ModelCollection = employees;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex}");
            }

            return query;
        }
    }
}