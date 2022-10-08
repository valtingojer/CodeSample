using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.Samples.SingletonPattern.Scripts
{
	public class ScoreTrigger : MonoBehaviour
	{
		[SerializeField]
		private int hitPoints = 1;
		private int cooldown = 2;
		private float time = 0;
		private float lastHit = 0;
		// OnTriggerEnter is called when the Collider other enters the trigger.
		protected void OnTriggerEnter(Collider other)
		{
			if(lastHit + cooldown > time)
				return;
				
			lastHit = time;
			
			if(other.CompareTag("Player"))
				ScoreManager.AddScore(hitPoints);
		}
		
		// Update is called every frame, if the MonoBehaviour is enabled.
		protected void Update()
		{
			time += Time.deltaTime;
		}
	}	
}


