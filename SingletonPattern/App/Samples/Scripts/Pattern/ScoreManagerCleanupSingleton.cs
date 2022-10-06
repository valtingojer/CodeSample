using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.Samples.SingletonPattern.Scripts
{
	public class ScoreManagerCleanupSingleton<T> : MonoBehaviour where T : MonoBehaviour
	{
		private static T _instance;
		
		private static T Instance 
		{
			get
			{
				if(_instance == null)
				{
					_instance = GameObject.FindObjectOfType<T>();
					if(Instance == null)
					{
						var go = new GameObject($"Instance of {typeof(T)}");
						_instance = go.AddComponent<T>();
					}
				}
				
				return _instance;
			}
		}
		
		void Awake()
		{
			if(_instance != null)
				Destroy(this.gameObject); //prevent duplicated silgleton
		}
	}	
}


