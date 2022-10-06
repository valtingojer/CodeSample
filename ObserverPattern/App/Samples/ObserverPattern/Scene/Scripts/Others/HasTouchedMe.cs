using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.Samples.ObserverPattern.Scripts
{
	public class HasTouchedMe : MonoBehaviour
	{
		private Material material;
		private Renderer renderer;
		private int m_hasTouched = -1;
		// Start is called before the first frame update
		void Start()
		{
			material = new Material(Shader.Find("Specular"));
			renderer = GetComponent<Renderer>();
		}

		// Update is called once per frame
		void Update()
		{
			int hasTouched = PlayerPrefs.GetInt($"{gameObject.name}-touched");
	    
			if(hasTouched == m_hasTouched)
				return;
	    	
			m_hasTouched = hasTouched;
	    
			if(hasTouched == 1)
				material.color = new Color(1,0,0,1);
			else
				material.color = new Color(0,1,0,1);
    	
			renderer.material = material;
    	
		}
	}
	
}

