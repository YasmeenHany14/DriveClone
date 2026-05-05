using DriveClone.Application.Common.Constants.ValidationMessages;
using DriveClone.Application.DTOs.AuthDtos;
using FluentValidation;

namespace DriveClone.Application.Validators.UserValidators;

public class RefreshRequestValidator : AbstractValidator<RefreshRequestDto>
{
    public RefreshRequestValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;
        ClassLevelCascadeMode = CascadeMode.Stop;

        RuleSet("Input", () =>
        {
            RuleFor(x => x.RefreshToken)
                .NotNull().WithMessage(AuthValidationErrorMessages.RefreshTokenRequired)
                .NotEmpty().WithMessage(AuthValidationErrorMessages.RefreshTokenRequired);
            
            RuleFor(x => x.AccessToken)
                .NotNull().WithMessage(AuthValidationErrorMessages.AccessTokenRequired)
                .NotEmpty().WithMessage(AuthValidationErrorMessages.AccessTokenRequired);
        });
    }
}
