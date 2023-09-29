using MediatR;
using System;
using UserManagement.Helper;
using UserManagement.Data.Dto.Action;

namespace UserManagement.MediatR.Commands
{
    public class AddActionCommand: IRequest<ServiceResponse<ActionDto>>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
