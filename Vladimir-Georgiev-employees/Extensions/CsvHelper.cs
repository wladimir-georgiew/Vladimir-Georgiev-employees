namespace Vladimir_Georgiev_employees.Extensions
{
    public static class CsvHelper
    {
        /// <summary>
        /// Converts a string date to a datetime type. Returns null if the converting fails.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DateTime? ConvertToDate(this string? value)
        {
            var isDate = DateTime.TryParse(value, out var date);

            if (!isDate) return null;

            return date;
        }
    }
}