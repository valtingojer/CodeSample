using System;

namespace App.Samples.UnitTestSample
{
	public static class DamageCalculator
	{
		public static int CalculateDamage(int amount, float mitigationPercent)
		{
			var mitigatedAmount = amount * (1f - mitigationPercent);
			int result = Convert.ToInt32(Math.Round(mitigatedAmount, 0));
			return result;
		}
		
		public static int CalculateDamage(int amount, ICharacter character)
		{
			var characterMitigation = (float)character.Inventory.GetTotalArmor() / 100f;
			var result = CalculateDamage(amount, characterMitigation);
			return result;
		}
	}
}