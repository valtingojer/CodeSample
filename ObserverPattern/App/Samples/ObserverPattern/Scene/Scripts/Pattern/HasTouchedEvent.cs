using System;
using UnityEngine;

namespace App.Samples.ObserverPattern.Scripts
{
	public class HasTouchedEvent : MonoBehaviour
	{
		public static event Action<string> OnTouched;
		
		private void OnTriggerEnter(Collider Other)
		{
			OnTouched?.Invoke(Other.gameObject.name);
		}
	}

}

