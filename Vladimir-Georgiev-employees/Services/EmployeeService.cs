using Vladimir_Georgiev_employees.Extensions;
using Vladimir_Georgiev_employees.Models.DTOs;
using Vladimir_Georgiev_employees.Services.Contracts;

namespace Vladimir_Georgiev_employees.Services
{
    public class EmployeeService : IEmployeeService
    {
        public IEnumerable<EmployeeCsv> LoadFromCsv(IFormFile csvFile)
        {
            var employees = new List<EmployeeCsv>();

            using (var fileStream = csvFile.OpenReadStream())
            using (var reader = new StreamReader(fileStream))
            {
                string row;
                reader.ReadLine();

                while ((row = reader.ReadLine()) != null)
                {
                    var values = row.Split(',');

                    var employeeId = Convert.ToString(values[0].Trim());
                    var projectId = Convert.ToString(values[1].Trim());
                    var dateFrom = values[2].ConvertToDate()
                        ?? throw new Exception($"The pair of Employee ID: {employeeId} and Project ID: {projectId} is missing data on the DateFrom column.");
                    var dateTo = values[3].ConvertToDate()
                        ?? DateTime.Now;

                    employees.Add(new EmployeeCsv()
                    {
                        EmployeeId = employeeId,
                        ProjectId = projectId,
                        DateFrom = dateFrom,
                        DateTo = dateTo,
                    });
                }
            }

            return employees;
        }

        public Dictionary<string, EmployeesPair?> GetLongestWorkingEmployeePairs(IEnumerable<EmployeeCsv> employees)
        {
            var allEmployeesByProject = employees.GroupBy(x => x.ProjectId);

            var longestEmployeePairsByProject = new Dictionary<string, EmployeesPair?>();

            // Traversing the groups of projects
            foreach (var projectEmployeesGroup in allEmployeesByProject)
            {
                var projectId = projectEmployeesGroup.Key;
                longestEmployeePairsByProject.Add(projectId, null);
                var currentProjectEmployees = projectEmployeesGroup.ToList();

                // Travsersing through the employees for each project
                for (int i = 0; i < projectEmployeesGroup.Count(); i++)
                {
                    var firstEmployee = currentProjectEmployees[i];

                    // Traversing through the employees for each employee
                    for (int j = 0; j < projectEmployeesGroup.Count(); j++)
                    {
                        var secondEmployee = currentProjectEmployees[j];

                        if (secondEmployee.EmployeeId == firstEmployee.EmployeeId)
                        {
                            continue;
                        }

                        // Getting the bigger start date and the smaller end date between both employees to get the total time they worked together
                        var startDate = firstEmployee.DateFrom > secondEmployee.DateFrom
                            ? firstEmployee.DateFrom
                            : secondEmployee.DateFrom;

                        var endDate = firstEmployee.DateTo < secondEmployee.DateTo
                            ? firstEmployee.DateTo
                            : secondEmployee.DateTo;

                        var totalDaysWorkingTogether = (endDate - startDate).Days;

                        // Set the current pair days working together only if they are more than the pair we have already set
                        if (totalDaysWorkingTogether > (longestEmployeePairsByProject[projectId]?.DaysWorkedTogether ?? 0))
                        {
                            longestEmployeePairsByProject[projectId] = new EmployeesPair
                            {
                                Employee1 = firstEmployee,
                                Employee2 = secondEmployee,
                                DaysWorkedTogether = totalDaysWorkingTogether
                            };
                        }
                    }
                }
            }

            return longestEmployeePairsByProject;
        }
    }
}