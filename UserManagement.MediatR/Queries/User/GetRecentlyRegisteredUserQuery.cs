﻿using MediatR;
using System.Collections.Generic;
using UserManagement.Data.Dto.User;

namespace UserManagement.MediatR.Queries
{
    public class GetRecentlyRegisteredUserQuery : IRequest<List<UserDto>>
    {
    }
}
