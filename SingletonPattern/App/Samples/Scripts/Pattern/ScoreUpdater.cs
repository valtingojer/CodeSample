using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace App.Samples.SingletonPattern.Scripts
{
	
	public class ScoreUpdater : MonoBehaviour
	{
		private int score;
		private TextMeshProUGUI text;
		
		// Start is called before the first frame update
		void Start()
		{
			score = ScoreManager.score;
			text = GetComponent<TextMeshProUGUI>();
			text.SetText(score.ToString());
		}

		// Update is called once per frame
		void Update()
		{
			if(score != ScoreManager.score)
			{
				score = ScoreManager.score;
				text.SetText(score.ToString());
			}
		}
	}
	
}

