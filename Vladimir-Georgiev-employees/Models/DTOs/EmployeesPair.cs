namespace Vladimir_Georgiev_employees.Models.DTOs
{
    public class EmployeesPair
    {
        public EmployeeCsv? Employee1 { get; set; }

        public EmployeeCsv? Employee2 { get; set; }

        public int DaysWorkedTogether { get; set; } = 0;
    }
}