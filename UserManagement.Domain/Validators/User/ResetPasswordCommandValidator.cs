using FluentValidation;
using UserManagement.Domain.Model.User;

namespace UserManagement.Domain.Validators.User
{
    public class ResetPasswordCommandValidator : AbstractValidator<ResetPasswordModel>
    {
        public ResetPasswordCommandValidator()
        {
            RuleFor(c => c.UserName).NotEmpty().WithMessage("UserName is required");
            RuleFor(c => c.Password).NotEmpty().WithMessage("Password is required");
        }
    }
}
