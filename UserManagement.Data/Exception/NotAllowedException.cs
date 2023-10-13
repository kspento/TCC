using System;
namespace UserManagement.Domain.Exception
{
    public class NotAllowedException : System.Exception
    {
        public NotAllowedException(string message) : base(message) { }
    }
}