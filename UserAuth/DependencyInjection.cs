using UserAuth.Application.Abstractions.Auth;
using UserAuth.Application.Abstractions.Behaviors;
using UserAuth.Domain.Abstractions;
using UserAuth.Domain.Users;
using UserAuth.Infrastructure.Repositories;
using UserAuth.Infrastructure.Auth;
using UserAuth.Infrastructure.Data;
using UserAuth.Infrastructure.Clock;
using UserAuth.Application.Abstractions.Clock;
using FluentValidation;
using UserAuth.Domain.Entry;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MediatR;
using System.Reflection;

namespace UserAuth;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        

        services.AddMediatR(typeof(DependencyInjection).Assembly);

        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        

        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

        return services;
    }

    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSignalR();

        AddPersistence(services, configuration);
        AddAuthentication(services, configuration);

        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<IEntryRepository, EntryRepository>();

        return services;
    }

    private static void AddPersistence(IServiceCollection services, IConfiguration configuration)
    {
        // SQLServer
        var connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new ArgumentNullException(nameof(configuration));

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());
;
    }

    private static void AddAuthentication(IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer();

        services.ConfigureOptions<JwtOptionsSetup>();
        services.ConfigureOptions<JwtBearerOptionsSetup>();
    }

}