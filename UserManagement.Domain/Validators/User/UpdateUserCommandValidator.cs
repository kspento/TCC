using FluentValidation;
using UserManagement.Domain.Model.User;

namespace UserManagement.Domain.Validators.User
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserModel>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(c => c.FirstName).NotEmpty().WithMessage("FirstName is Required");
            RuleFor(c => c.LastName).NotEmpty().WithMessage("LastName is Required");
        }
    }
}
