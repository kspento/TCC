using FluentValidation;
using UserManagement.Domain.Model.User;

namespace UserManagement.Domain.Validators.User
{
    public class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordModel>
    {
        public ChangePasswordCommandValidator()
        {
            RuleFor(c => c.UserName).NotEmpty().WithMessage("Username is required.");
            RuleFor(c => c.OldPassword).NotEmpty().WithMessage("Old Password is required.");
            RuleFor(c => c.NewPassword).NotEmpty().WithMessage("New Password is required.");
        }
    }
}
