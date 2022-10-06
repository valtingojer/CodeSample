using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.Samples.ObserverPattern.Scripts
{
	
	public class HasTouchedEventListener : MonoBehaviour
	{
		// Start is called on the frame when a script is enabled just before any of the Update methods is called the first time.
		protected void Start()
		{
			//For the sake of the sample, we clear out the playerprefs
			PlayerPrefs.DeleteAll();
		}
		// This function is called when the object becomes enabled and active.
		protected void OnEnable()
		{
			HasTouchedEvent.OnTouched += SetAsTouched;
		}
	
		// This function is called when the behaviour becomes disabled () or inactive.
		protected void OnDisable()
		{
			HasTouchedEvent.OnTouched -= SetAsTouched;
		}
	
		private void SetAsTouched(string name)
		{
			PlayerPrefs.SetInt($"{name}-touched", 1);
		}
	}
}

