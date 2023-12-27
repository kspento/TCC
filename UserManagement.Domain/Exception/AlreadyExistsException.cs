using System;
namespace UserManagement.Domain.Exception
{
    public class AlreadyExistsException : System.Exception
    {
        public AlreadyExistsException(string message) : base(message) { }
    }
}