using Backend.TechChallenge.Core.Models;
using System;
using System.Collections.Generic;

namespace Backend.TechChallenge.Core.Services
{

	public class BonusCalculatorSuperUser : IBonusCalculator
	{
		public decimal Calculate(decimal money)
		{
			if (money > 100)
			{
				var percentage = Convert.ToDecimal(0.20);
				return money * percentage;
			}

			return 0;
		}
	}
}