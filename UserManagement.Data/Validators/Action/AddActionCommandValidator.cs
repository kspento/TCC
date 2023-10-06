using UserManagement.MediatR.Commands;
using FluentValidation;

namespace UserManagement.Domain.Validators.Action
{
    public class AddActionCommandValidator : AbstractValidator<AddActionCommand>
    {
        public AddActionCommandValidator()
        {
            RuleFor(c => c.Name).NotEmpty().WithMessage("Name is required");
        }
    }
}
