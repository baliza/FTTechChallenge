using System;

namespace Backend.TechChallenge.Core.Services
{
	public class BonusCalculatorNormal : IBonusCalculator
	{
		public decimal Calculate(decimal money)
		{
			if (money > 100)
			{
				if (money > 100)
				{
					var percentage = Convert.ToDecimal(0.12);
					return money * percentage;
				}
				if (money < 100)
				{
					if (money > 10)
					{
						var percentage = Convert.ToDecimal(0.8);
						return money * percentage;
					}
				}
			}
			return 0;
		}
	}
}