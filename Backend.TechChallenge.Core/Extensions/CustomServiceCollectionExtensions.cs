using Backend.TechChallenge.Core.Services;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Backend.TechChallenge.Core.Extensions
{
	public static class CustomServiceCollectionExtensions
	{
		public static IServiceCollection AddDomainDependencies(this IServiceCollection services)
		{
			if (services == null)
			{
				throw new ArgumentNullException("services");
			}

			services.AddTransient<IEmailFixer, EmailFixer>();
			services.AddTransient<IUserFactory, UserFactory>();
			return services;
		}
	}
}