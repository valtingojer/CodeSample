using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace App.Samples.WeaponInterface.Scripts
{
	public class MissileLauncher : MonoBehaviour, ILauncher
	{
		[SerializeField]
		private GameObject missilePrefab;
		
		public void Launch(Weapon weapon)
		{
			Transform target = GameObject.FindGameObjectsWithTag("Respawn")[0].transform;
			var missile = Instantiate(missilePrefab, weapon.transform.position, weapon.transform.rotation);
			missile.GetComponent<ProjectilController>().SetTarget(target);
		}
	}
}

