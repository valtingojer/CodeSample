using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.Samples.WeaponInterface.Scripts
{
	public class PlayerController : MonoBehaviour
	{	
		[SerializeField]
		[Range(1, 10)]
		private int speed = 5;
		
		[SerializeField]
		[Range(1,10)]
		private int factor = 1;
		
		private float delta => speed * factor * Time.deltaTime; 
		
		private CharacterController controller;
		
		private void Start()
		{
			controller = gameObject.AddComponent<CharacterController>();
		}


		// Update is called once per frame
		void Update()
		{
			Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
			controller.Move(move * delta);
		}
	}

}
