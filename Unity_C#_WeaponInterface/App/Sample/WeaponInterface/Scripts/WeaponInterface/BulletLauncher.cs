using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.Samples.WeaponInterface.Scripts
{
	public class BulletLauncher : MonoBehaviour, ILauncher
	{
		[SerializeField]
		private GameObject bulletPrefab;
		
		public void Launch(Weapon weapon)
		{
			var bullet = Instantiate(bulletPrefab, weapon.transform.position, weapon.transform.rotation);
			bullet.GetComponent<ProjectilController>().Move();
		}
	}
}

