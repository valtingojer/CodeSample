using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.Samples.WeaponInterface.Scripts
{
	public class Weapon : MonoBehaviour
	{
		private ILauncher launcher;
		
		[SerializeField]
		[Range(1,20)]
		private int fireRefreshRate = 10;
		[Range(1,40)]
		private int fireRateFactor = 10;
		
		private float fireRateDelta => (float)fireRefreshRate / fireRateFactor;
		
		private float nextFireTime;
		
		// Start is called before the first frame update
		void Start()
		{
			launcher = GetComponent<ILauncher>();
		}

		// Update is called once per frame
		void Update()
		{
			if(CanFire())
				Fire();
		}
		
		private bool CanFire()
		{
			bool didPressButton = Input.GetButtonDown("Fire1");
			bool isNextFireTime = Time.time >= nextFireTime;
			return didPressButton && isNextFireTime;
		}
		
		private void Fire()
		{
			nextFireTime = Time.time + fireRateDelta;
			launcher.Launch(this);
		}
	}
}

