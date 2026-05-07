using DriveClone.Application.Common.Constants.ValidationMessages;
using DriveClone.Application.DTOs.UserDtos;
using FluentValidation;

namespace DriveClone.Application.Validators.UserValidators;

public class UpdatePasswordRequestValidator : AbstractValidator<UpdatePasswordRequestAppDto>
{
    public UpdatePasswordRequestValidator()
    {
        RuleSet("Input", () =>
        {
            RuleFor(x => x.CurrentPassword)
                .NotNull().WithMessage(string.Format(CommonValidationErrorMessages.NotNull,
                    nameof(UpdatePasswordRequestAppDto.CurrentPassword)))
                .NotEmpty().WithMessage(string.Format(CommonValidationErrorMessages.NotEmpty,
                    nameof(UpdatePasswordRequestAppDto.CurrentPassword)));
                
            RuleFor(x => x.NewPassword)
                .NotNull().WithMessage(string.Format(CommonValidationErrorMessages.NotNull,
                    nameof(UpdatePasswordRequestAppDto.NewPassword)))
                .NotEmpty().WithMessage(string.Format(CommonValidationErrorMessages.NotEmpty,
                    nameof(UpdatePasswordRequestAppDto.NewPassword)))
                .MinimumLength(6).WithMessage(string.Format(AuthValidationErrorMessages.InvalidLength, 6))
                .Matches(@"[A-Z]").WithMessage(AuthValidationErrorMessages.UpperCaseRequired)
                .Matches(@"[0-9]").WithMessage(AuthValidationErrorMessages.DigitRequired)
                .Matches(@"[!@#$%^&*(),.?""{}|<>]").WithMessage(AuthValidationErrorMessages.SpecialCharacterRequired);
            
            RuleFor(x => x.ConfirmPassword)
                .NotNull().WithMessage(string.Format(CommonValidationErrorMessages.NotNull, nameof(UpdatePasswordRequestAppDto.ConfirmPassword)))
                .NotEmpty().WithMessage(string.Format(CommonValidationErrorMessages.NotEmpty, nameof(UpdatePasswordRequestAppDto.ConfirmPassword)))
                .Equal(x => x.NewPassword).WithMessage(AuthValidationErrorMessages.PasswordsDoNotMatch);
        });
    }
}
