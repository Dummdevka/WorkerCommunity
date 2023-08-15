using Application.Behaviors;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
	public static IServiceCollection addApplication(this IServiceCollection services) {
		var assembly = typeof(DependencyInjection).Assembly;

		return services.AddMediatR(cfg => 
			cfg.RegisterServicesFromAssembly(assembly))
			.AddValidatorsFromAssembly(assembly)
			.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
    }
}

