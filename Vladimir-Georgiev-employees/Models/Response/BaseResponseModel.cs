namespace Vladimir_Georgiev_employees.Models.Response
{
    public class BaseResponseModel<T>
    {
        public BaseResponseModel(T? data, bool isSuccess = true, string? errorMessage = "")
        {
            Data = data;
            IsSuccess = isSuccess;
            ErrorMessage = errorMessage;

        }

        public T? Data { get; set; }

        public bool IsSuccess { get; set; }

        public string? ErrorMessage { get; set; }
    }
}