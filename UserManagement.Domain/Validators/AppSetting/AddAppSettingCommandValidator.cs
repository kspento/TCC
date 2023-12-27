using FluentValidation;
using UserManagement.MediatR.Commands;

namespace UserManagement.Domain.Validators.AppSetting
{
    public class AddAppSettingCommandValidator : AbstractValidator<AddAppSettingCommand>
    {
        public AddAppSettingCommandValidator()
        {
            RuleFor(c => c.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(c => c.Key).NotEmpty().WithMessage("Key is required");
            RuleFor(c => c.Value).NotEmpty().WithMessage("Value is required");
        }
    }
}
