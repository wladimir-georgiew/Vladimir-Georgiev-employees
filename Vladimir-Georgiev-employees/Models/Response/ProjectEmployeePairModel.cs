using Vladimir_Georgiev_employees.Models.DTOs;

namespace Vladimir_Georgiev_employees.Models.Response
{
    public class ProjectEmployeePairModel
    {
        public required string ProjectId { get; set; }

        public required EmployeesPair? EmployeesPair { get; set; }
    }
}