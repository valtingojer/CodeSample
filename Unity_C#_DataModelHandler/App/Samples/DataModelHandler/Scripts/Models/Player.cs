using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace App.Samples.DataModelHandler.Scripts
{
	public class Player : MonoBehaviour
	{
		private PlayerModel playerModel;
		[SerializeField]
		private TextMeshProUGUI healthBarText;
		
		
		// Awake is called when the script instance is being loaded.
		protected void Awake()
		{
			playerModel = PlayerPersistence.GetData();
			UpdateHealthBar();
		}
		
		// This function is called when the behaviour becomes disabled () or inactive.
		protected void OnDisable()
		{
			PlayerPersistence.SaveData(playerModel);
		}
		
		private void UpdateHealthBar()
		{
			healthBarText.SetText(playerModel.Health.ToString());
		}
		
		public void ModifyHealth(int amounth)
		{
			int newHealth = playerModel.Health + amounth;
			playerModel.Health = System.Math.Max(0, newHealth);
			UpdateHealthBar();
		}
		
		
		public void ResetHealth()
		{
			playerModel = PlayerPersistence.GetNewPlayer();
			UpdateHealthBar();
		}
	}
}