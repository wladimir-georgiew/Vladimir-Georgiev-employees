using Vladimir_Georgiev_employees.Models.DTOs;

namespace Vladimir_Georgiev_employees.Services.Contracts
{
    public interface IEmployeeService
    {
        IEnumerable<EmployeeCsv> LoadFromCsv(IFormFile csvFile);
        Dictionary<string, EmployeesPair?> GetLongestWorkingEmployeePairs(IEnumerable<EmployeeCsv> employees);
    }
}