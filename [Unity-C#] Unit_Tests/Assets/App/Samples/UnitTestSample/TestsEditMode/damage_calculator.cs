using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace App.Samples.UnitTestSample
{
	public class damage_calculator
	{
		[Test]
		public void set_damage_to_half_with_50_percent_mitigation()
		{
			int finalDamage5 = DamageCalculator.CalculateDamage(10, 0.5f);
			Assert.AreEqual(5, finalDamage5);
			
			int finalDamage6 = DamageCalculator.CalculateDamage(13, 0.5f);
			Assert.AreEqual(6, finalDamage6);
			
			int finalDamage7 = DamageCalculator.CalculateDamage(14, 0.5f);
			Assert.AreEqual(7, finalDamage7);
		}
		
		[Test]
		public void calculate_2_damage_from_10_with_80_percent_mitigation()
		{
			int finalDamage2 = DamageCalculator.CalculateDamage(10, 0.8f);
			Assert.AreEqual(2, finalDamage2);
		}
		

	}
}