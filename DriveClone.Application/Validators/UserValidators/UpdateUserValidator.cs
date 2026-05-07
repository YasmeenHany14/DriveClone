using System.Runtime.InteropServices.ComTypes;
using System.Xml;
using DriveClone.Application.Common.Constants.ValidationMessages;
using DriveClone.Application.DTOs.AuthDtos;
using DriveClone.Application.DTOs.UserDtos;
using DriveClone.Domain.Models;
using DriveClone.Domain.Shared.Constraints;
using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace DriveClone.Application.Validators.UserValidators;

public class UpdateUserValidator : AbstractValidator<UpdateUserAppDto>
{
    public UpdateUserValidator(UserManager<User> userManager)
    {
        RuleSet("Input", () =>
        {
            RuleFor(u => u.FirstName)
                .NotNull().WithMessage(string.Format(CommonValidationErrorMessages.NotNull, nameof(UpdateUserAppDto.FirstName)))
                .NotEmpty().WithMessage(string.Format(CommonValidationErrorMessages.NotEmpty, nameof(UpdateUserAppDto.FirstName)))
                .MaximumLength(UserConstraints.NameMaxLength).WithMessage(string.Format(CommonValidationErrorMessages.StringLength, nameof(CreateUserAppDto.FirstName), 50));

            RuleFor(u => u.LastName)
                .NotNull().WithMessage(string.Format(CommonValidationErrorMessages.NotNull, nameof(UpdateUserAppDto.LastName)))
                .NotEmpty().WithMessage(string.Format(CommonValidationErrorMessages.NotEmpty, nameof(UpdateUserAppDto.LastName)))
                .MaximumLength(UserConstraints.UsernameMaxLength).WithMessage(string.Format(CommonValidationErrorMessages.StringLength, nameof(CreateUserAppDto.LastName), 50));
            RuleFor(x => x.Email)
                .NotNull().WithMessage(string.Format(CommonValidationErrorMessages.NotNull, nameof(UpdateUserAppDto.Email)))
                .NotEmpty().WithMessage(string.Format(CommonValidationErrorMessages.NotEmpty, nameof(UpdateUserAppDto.Email)))
                .EmailAddress().WithMessage(string.Format(CommonValidationErrorMessages.InvalidEmail,  nameof(UpdateUserAppDto.Email)));
        });

        RuleSet("UpdateBusiness", () =>
        {
            RuleFor(x => x.Email)
                .MustAsync(async (dto, _, context, _) =>
                {
                    var userId = (string)context.RootContextData["userId"];
                    var user = await userManager.FindByEmailAsync(dto.Email!);
                    if (user == null)
                        return true;
                    return user.Email == dto.Email && user.Id == userId;
                }).WithMessage(AuthValidationErrorMessages.EmailTaken);
        });
    }
}
