using DriveClone.Application.Common.Constants.Errors;
using DriveClone.Application.Common.ErrorAndResults;
using FluentValidation;
using FluentValidation.Internal;

namespace DriveClone.Application.Helpers;

public static class ValidationHelper
{
    public static async Task<Result> ValidateAndReportAsync<T>(IValidator<T> validator, T dto, string ruleSet = "Input")
    {
        var validationResult = await validator.ValidateAsync(
            dto,
            options => options.IncludeRuleSets(ruleSet)
        );
        return BuildResult(validationResult);
    }

    public static async Task<Result> ValidateAndReportAsync<T>(
        IValidator<T> validator,
        T dto,
        Action<ValidationContext<T>>? contextSetup = null,
        string ruleSet = "Input")
    {
        var context = new ValidationContext<T>(dto, new PropertyChain(), new RulesetValidatorSelector(new[] { ruleSet }));
        contextSetup?.Invoke(context);

        var validationResult = await validator.ValidateAsync(context);
        return BuildResult(validationResult);
    }

    private static Result BuildResult(FluentValidation.Results.ValidationResult validationResult)
    {
        if (validationResult.IsValid)
            return Result.Success();
        
        var errors = validationResult.Errors
            .GroupBy(e => e.PropertyName)
            .ToDictionary(
                g => g.Key,
                g => g.Select(e => e.ErrorMessage).ToArray()
                );
        return Result.Failure(CommonErrors.ValidationProblem(errors));
    }
}
