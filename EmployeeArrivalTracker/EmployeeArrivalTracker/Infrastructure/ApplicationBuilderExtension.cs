namespace EmployeeArrivalTracker.Infrastructure
{
    using System.Reflection;

    using EmployeeArrivalTracker.Data;
    using EmployeeArrivalTracker.Data.Models;
    using EmployeeArrivalTracker.Models;
    using Microsoft.EntityFrameworkCore;
    using Newtonsoft.Json;

    public static class ApplicationBuilderExtension
    {
        public static IApplicationBuilder PrepareDatabase(this IApplicationBuilder app)
        {
            using var scopeServices = app.ApplicationServices.CreateScope();
            var services = scopeServices.ServiceProvider;

            var data = scopeServices.ServiceProvider.GetService<EmployeeArrivalTrackerDbContext>();

            MigrateDatabase(services);

            SeedEmployees(services);

            return app;
        }

        private static void MigrateDatabase(IServiceProvider services)
        {
            var data = services.GetRequiredService<EmployeeArrivalTrackerDbContext>();

            data.Database.Migrate();
        }

        private static void SeedEmployees(IServiceProvider services)
        {
            var data = services.GetRequiredService<EmployeeArrivalTrackerDbContext>();

            if (data.Employees.AsNoTracking().Any())
            {
                return;
            }

            var assemblyPath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            var resourcesPath = Path.Combine(assemblyPath, "Resources");
            var filePath = Path.Combine(resourcesPath, "employees.json");

            using (StreamReader reader = new StreamReader(filePath))
            {
                string json = reader.ReadToEnd();

                var employeesCollection = JsonConvert.DeserializeObject<List<EmployeeParse>>(json);

                var tempEmployeeCollection = new List<Employee>();
                var tempTeamsCollection = new List<Team>();

                foreach (var employee in employeesCollection)
                {
                    var existingEmployee = data.Employees.AsNoTracking().FirstOrDefault(e => e.EmployeeId == employee.Id);

                    if (existingEmployee == null)
                    {
                        Employee tempEmployee = new Employee
                        {
                            EmployeeId = employee.Id,
                            Age = employee.Age,
                            Email = employee.Email,
                            ManagerId = employee.ManagerId,
                            Name = employee.Name,
                            SurName = employee.SurName,
                            Role = employee.Role
                        };

                        foreach (var team in employee.Teams)
                        {
                            Team tempTeam = new Team
                            {
                                Name = team
                            };

                            tempTeamsCollection.Add(tempTeam);
                            tempEmployee.Teams.Add(tempTeam);                          
                        }

                        data.Employees.Add(tempEmployee);
                        
                        tempEmployeeCollection.Add(tempEmployee);
                    }
                }

                data.Teams.AddRange(tempTeamsCollection);
                data.Employees.AddRange(tempEmployeeCollection);
            }

            data.SaveChanges();
        }
    }
}
