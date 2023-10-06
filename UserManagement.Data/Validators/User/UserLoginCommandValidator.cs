using FluentValidation;
using UserManagement.Domain.Model.User;

namespace UserManagement.Domain.Validators.User
{
    public class UserLoginCommandValidator : AbstractValidator<UserLoginModel>
    {
        public UserLoginCommandValidator()
        {
            RuleFor(c => c.UserName).NotEmpty().WithMessage("Please enter username.");
            RuleFor(c => c.Password).NotEmpty().WithMessage("Please enter password.");
        }
    }
}
