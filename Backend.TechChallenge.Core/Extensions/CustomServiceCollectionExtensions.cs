﻿using Backend.TechChallenge.Api.Services;
using Backend.TechChallenge.Core.Models;
using Backend.TechChallenge.Core.Services;
using Backend.TechChallenge.Core.Tools;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

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
			services.AddTransient<IUserService, UserService>();
			services.AddTransient<IDictionary<UserType, IBonusCalculator>>(BuildBonusCalculator);
			return services;
		}

		private static IDictionary<UserType, IBonusCalculator> BuildBonusCalculator(IServiceProvider arg)
		{
			return new Dictionary<UserType, IBonusCalculator> {
				{ UserType.Normal,new BonusCalculatorNormal() },
				{ UserType.Premium,new BonusCalculatorPremiun() },
				{ UserType.SuperUser,new BonusCalculatorSuperUser() }
				};
		}
	}
}