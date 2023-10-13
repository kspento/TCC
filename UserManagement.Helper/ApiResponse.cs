using System.Collections.Generic;

namespace UserManagement.Helper
{
    public class ApiResponse<T>
    {
        public T Data { get; set; }
        public int StatusCode { get; set; } = 200;
        public List<string> Errors { get; set; } = new List<string>();
    }
}
