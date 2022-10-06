using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.Samples.SingletonPattern.Scripts
{
	public class ScoreManager : ScoreManagerCleanupSingleton<ScoreManager>
	{
		public static int score { get; private set; } = 0;
		
		public static void AddScore(int points)
		{
			score += points;
		}
	}	
}


