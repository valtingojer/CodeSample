using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.Samples.WeaponInterface.Scripts
{
	public class ProjectilController : MonoBehaviour
	{
		[SerializeField]
		[Range(1,10)]
		private int speed = 5;
		
		private bool moveForward = false;
		private Transform _target = null;
		
		public void Move()
		{
			moveForward = true;
		}
		
		public void SetTarget(Transform target)
		{
			_target = target;
		}
		
		public void moveDirection()
		{
			if(!moveForward)
				return;
			transform.position += Vector3.forward * Time.deltaTime * speed;

		}
		
		public void moveToTarget()
		{
			if(_target == null)
				return;
			
			// Move our position a step closer to the target.
			var step =  speed * Time.deltaTime; // calculate distance to move
			transform.position = Vector3.MoveTowards(transform.position, _target.position, step);
		}
		
		// Update is called every frame, if the MonoBehaviour is enabled.
		protected void Update()
		{
			moveDirection();
			moveToTarget();
		}
	}	
}


