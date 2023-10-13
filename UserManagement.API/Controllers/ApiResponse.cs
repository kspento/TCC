namespace UserManagement.API.Controllers
{
    internal class ApiResponse<T>
    {
        public T Data { get; set; }
        public int StatusCode { get; set; }

        public bool Success { get; set; }
        public object Errors { get; set; }
    }
}