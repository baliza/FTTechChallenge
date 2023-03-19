using Backend.TechChallenge.Api.Services;
using Backend.TechChallenge.Core.ExternalServices;
using Backend.TechChallenge.Infra.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;

namespace Backend.TechChallenge.Api.Extensions
{
	public static class CustomServiceCollectionExtensions
	{
		public static IServiceCollection AddApplicationDependencies(this IServiceCollection services, IConfiguration configuration)
		{
			if (services == null)
			{
				throw new ArgumentNullException("services");
			}

			services.AddTransient<IRequestValidator, RequestValidator>();
			var storage = configuration.GetSection("GeneralConfiguration").GetValue<string>("Storage");
			if (storage == "FileDb")
			{
				services.AddSingleton((Func<IServiceProvider, IUsersRepository>)(_ =>
				{
					var path = configuration.GetSection("FileDbSettings").GetValue<string>("ConnectionString");
					string currentDirectory = Directory.GetCurrentDirectory();
					return new UsersFileRepository(currentDirectory + path);
				}));
			}
			else //InMemory
			{
				services.AddSingleton<IUsersRepository, UsersInMemoryRepository>();
			}

			return services;
		}
	}
}