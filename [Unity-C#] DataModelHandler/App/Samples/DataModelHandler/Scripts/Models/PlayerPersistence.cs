using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.Samples.DataModelHandler.Scripts
{
	public class PlayerPersistence
	{
		internal static PlayerModel GetNewPlayer()
		{
			var result = new PlayerModel(){
				Health = 100,
				Mana = 100,
				MovimentSpeed = 5
			};
			
			return result;
		}
		
		internal static PlayerModel GetPlayerFromStorage()
		{
			var result = new PlayerModel(){
				Health = PlayerPrefs.GetInt("Health"),
				Mana = PlayerPrefs.GetInt("Mana"),
				MovimentSpeed = PlayerPrefs.GetInt("MovimentSpeed")
			};
			
			return result;
		}
		
		internal static PlayerModel GetData()
		{
			PlayerModel result = null;
			if(PlayerPrefs.HasKey("Health") == false)
			{
				result = GetNewPlayer();
			}
			else
			{
				result = GetPlayerFromStorage();
			}
			
			return result;
		}
		
		internal static void SaveData(PlayerModel playerModel)
		{
			PlayerPrefs.SetInt("Health", playerModel.Health);
			PlayerPrefs.SetInt("Mana", playerModel.Mana);
			PlayerPrefs.SetInt("MovimentSpeed", playerModel.MovimentSpeed);
		}
	}
}
