using Application.Behaviors;
//using Application.Behaviours;
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
			.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>))
			.AddTransient(typeof(IPipelineBehavior<,>), typeof(CachingBehavior<,>));
    }
}

