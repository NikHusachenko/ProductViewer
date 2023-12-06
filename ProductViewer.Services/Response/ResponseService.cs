namespace ProductViewer.Services.Response
{
    public class ResponseService
    {
        public bool IsError { get; set; }
        public string ErrorMessage { get; set; }

        public static ResponseService Ok() => new ResponseService();
        public static ResponseService Error(string errorMessage)
            => new ResponseService()
            {
                ErrorMessage = errorMessage,
                IsError = true,
            };
    }

    public class ResponseService<T>
    {
        public bool IsError { get; set; }
        public string ErrorMessage { get; set; }
        public T Value { get; set; }

        public static ResponseService<T> Ok(T Value)
            => new ResponseService<T>()
            {
                Value = Value,
            };

        public static ResponseService<T> Error(string errorMessage)
            => new ResponseService<T>()
            {
                ErrorMessage = errorMessage,
                IsError = true,
            };
    }
}