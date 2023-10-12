﻿using System;
using UserManagement.Data.Dto.Page;

namespace UserManagement.Domain.Model.Page
{
    public class UpdatePageModel : PageDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
    }
}
