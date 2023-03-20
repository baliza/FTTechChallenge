namespace Backend.TechChallenge.Core.Services
{
	public class BonusCalculatorPremiun : IBonusCalculator
	{
		public decimal Calculate(decimal money)
		{
			if (money > 100)
			{
				return money * 2;
			}
			return 0;
		}
	}
}