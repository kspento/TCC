using UserManagement.MediatR.Commands;
using FluentValidation;

namespace UserManagement.Domain.Validators.Role
{
    public class AddRoleCommandValidator : AbstractValidator<AddRoleCommand>
    {
        public AddRoleCommandValidator()
        {
            RuleFor(c => c.Name).NotEmpty().WithMessage("Role Name is required.");
        }
    }
}
