using Microsoft.AspNetCore.Mvc;
using Vladimir_Georgiev_employees.Models.DTOs;
using Vladimir_Georgiev_employees.Models.Response;
using Vladimir_Georgiev_employees.Services.Contracts;

namespace Vladimir_Georgiev_employees.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEmployeeService _employeeService;

        public HomeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        public IActionResult Index()
        {
            return View(new BaseResponseModel<IEnumerable<ProjectEmployeePairModel>>(null));
        }

        [HttpPost]
        public IActionResult Index(IFormFile csvFile)
        {
            BaseResponseModel<IEnumerable<ProjectEmployeePairModel>> response;

            try
            {
                var employees = _employeeService.LoadFromCsv(csvFile);

                if (employees == null || employees.Count() <= 0)
                {
                    return View(new BaseResponseModel<IEnumerable<ProjectEmployeePairModel>>(null));
                }

                var items = _employeeService
                    .GetLongestWorkingEmployeePairs(employees)
                    .Select(x => new ProjectEmployeePairModel
                    {
                        ProjectId = x.Key,
                        EmployeesPair = x.Value,
                    })
                    .OrderByDescending(x => x.EmployeesPair?.DaysWorkedTogether)
                    .ToList();

                response = new BaseResponseModel<IEnumerable<ProjectEmployeePairModel>>(items);
            }
            catch (Exception ex)
            {
                response = new BaseResponseModel<IEnumerable<ProjectEmployeePairModel>>(null, false, ex.Message);
            }

            return View(response);
        }
    }
}