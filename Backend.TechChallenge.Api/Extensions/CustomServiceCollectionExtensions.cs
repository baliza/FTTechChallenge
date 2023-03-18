using Backend.TechChallenge.Api.Services;
using Backend.TechChallenge.Core.ExternalServices;
using Backend.TechChallenge.Infra.Repository;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Backend.TechChallenge.Api.Extensions
{
	public static class CustomServiceCollectionExtensions
	{
		public static IServiceCollection AddApplicationDependencies(this IServiceCollection services)
		{
			if (services == null)
			{
				throw new ArgumentNullException("services");
			}

			services.AddTransient<IRequestValidator, RequestValidator>();
			services.AddTransient<IUsersRepository, UsersFileRepository>();
			return services;
		}
	}
}