using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroy : MonoBehaviour
{
	[SerializeField]
	[Range(0,4)]
	private int delay = 2;
	
    
	// This function is called when the object becomes enabled and active.
	protected void OnEnable()
	{
		if(delay == 0)
			return;
		Destroy(gameObject, delay);
	}
}
