using DriveClone.Application.Common.Constants.ValidationMessages;
using DriveClone.Application.DTOs.AuthDtos;
using DriveClone.Domain.Models;
using DriveClone.Domain.Shared.Constraints;
using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace DriveClone.Application.Validators.UserValidators;

public class CreateUserValidator : AbstractValidator<CreateUserAppDto>
{
    public CreateUserValidator(
        UserManager<User> userManager)
    {
        // RuleLevelCascadeMode = CascadeMode.Stop;
        // ClassLevelCascadeMode = CascadeMode.Stop;
        
        RuleSet("Input", () =>
        {
            RuleFor(u => u.FirstName)
                .NotNull().WithMessage(string.Format(CommonValidationErrorMessages.NotNull, nameof(CreateUserAppDto.FirstName)))
                .NotEmpty().WithMessage(string.Format(CommonValidationErrorMessages.NotEmpty, nameof(CreateUserAppDto.FirstName)))
                .MaximumLength(UserConstraints.NameMaxLength).WithMessage(string.Format(CommonValidationErrorMessages.StringLength, nameof(CreateUserAppDto.FirstName), 50));

            RuleFor(u => u.LastName)
                .NotNull().WithMessage(string.Format(CommonValidationErrorMessages.NotNull, nameof(CreateUserAppDto.LastName)))
                .NotEmpty().WithMessage(string.Format(CommonValidationErrorMessages.NotEmpty, nameof(CreateUserAppDto.LastName)))
                .MaximumLength(UserConstraints.UsernameMaxLength).WithMessage(string.Format(CommonValidationErrorMessages.StringLength, nameof(CreateUserAppDto.LastName), 50));
            RuleFor(x => x.Email)
                .NotNull().WithMessage(string.Format(CommonValidationErrorMessages.NotNull, nameof(CreateUserAppDto.Email)))
                .NotEmpty().WithMessage(string.Format(CommonValidationErrorMessages.NotEmpty, nameof(CreateUserAppDto.Email)))
                .EmailAddress().WithMessage(string.Format(CommonValidationErrorMessages.InvalidEmail,  nameof(CreateUserAppDto.Email)));

            RuleFor(x => x.UserName)
                .NotNull().WithMessage(string.Format(CommonValidationErrorMessages.NotNull, nameof(CreateUserAppDto.UserName)))
                .NotEmpty().WithMessage(string.Format(CommonValidationErrorMessages.NotEmpty, nameof(CreateUserAppDto.UserName)))
                .MinimumLength(UserConstraints.UsernameMinLength)
                    .WithMessage(string.Format(CommonValidationErrorMessages.Range, nameof(CreateUserAppDto.UserName), UserConstraints.UsernameMinLength, UserConstraints.UsernameMaxLength))
                .MaximumLength(UserConstraints.UsernameMaxLength)
                    .WithMessage(string.Format(CommonValidationErrorMessages.Range, nameof(CreateUserAppDto.UserName), UserConstraints.UsernameMinLength, UserConstraints.UsernameMaxLength))
                .Matches(@"^[a-zA-Z]").WithMessage(AuthValidationErrorMessages.UsernameMustStartWithLetter)
                .Matches(@"^[a-zA-Z0-9_]*$").WithMessage(AuthValidationErrorMessages.UsernameNoSpecialCharacters);
            
            RuleFor(x => x.Password)
                .NotNull().WithMessage(string.Format(CommonValidationErrorMessages.NotNull,
                    nameof(CreateUserAppDto.Password)))
                .NotEmpty().WithMessage(string.Format(CommonValidationErrorMessages.NotEmpty,
                    nameof(CreateUserAppDto.Password)))
                
                .MinimumLength(6).WithMessage(string.Format(AuthValidationErrorMessages.InvalidLength, 6))
                .Matches(@"[A-Z]").WithMessage(AuthValidationErrorMessages.UpperCaseRequired)
                .Matches(@"[0-9]").WithMessage(AuthValidationErrorMessages.DigitRequired)
                .Matches(@"[!@#$%^&*(),.?""{}|<>]").WithMessage(AuthValidationErrorMessages.SpecialCharacterRequired);
            
            RuleFor(x => x.ConfirmPassword)
                .NotNull().WithMessage(string.Format(CommonValidationErrorMessages.NotNull, nameof(CreateUserAppDto.ConfirmPassword)))
                .NotEmpty().WithMessage(string.Format(CommonValidationErrorMessages.NotEmpty, nameof(CreateUserAppDto.ConfirmPassword)))
                .Equal(x => x.Password).WithMessage(AuthValidationErrorMessages.PasswordsDoNotMatch);
        });

        RuleSet("CreateBusiness", () =>
        {
            RuleFor(x => x.Email)
                .MustAsync(async (email, cancellation) =>
                    await userManager.FindByEmailAsync(email) == null)
                .WithMessage(AuthValidationErrorMessages.UserAlreadyExists);

            RuleFor(x => x.UserName)
                .MustAsync(async (username, _) =>
                    await userManager.FindByNameAsync(username) == null)
                .WithMessage(AuthValidationErrorMessages.UsernameExists);
        });
    }    
}
