﻿using System;
namespace UserManagement.Domain.Exception
{
    public class NotFoundException : System.Exception
    {
        public NotFoundException(string message) : base(message) { }
    }
}