using DriveClone.Application.Contracts;
using DriveClone.Application.DTOs.AuthDtos;
using DriveClone.Application.Services;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace DriveClone.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(cfg => { }, typeof(DependencyInjection).Assembly);
        services.AddValidatorsFromAssemblyContaining<CreateUserAppDto>();
        
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IGenerateTokenService, GenerateTokenService>();
        services.AddScoped<IUserService, UserService>();
        return services;
    }
}
