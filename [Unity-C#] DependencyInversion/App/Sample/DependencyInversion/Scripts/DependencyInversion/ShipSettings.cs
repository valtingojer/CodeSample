using UnityEngine;

namespace App.Samples.DependencyInversion.Scripts
{
	//This line create a new entry on the Unity create context menu
	//comment or uncoment it to show or hide this entry
	//[CreateAssetMenu(menuName = "Ship/Settings", fileName = "ShipData")]
	public class ShipSettings : ScriptableObject
	{
		[SerializeField]
		[Range(1,100)]
		private int turnSpeed = 25;
		[SerializeField]
		[Range(1,100)]
		private int moveSpeed = 10;
		[SerializeField]
		private bool useAi = false;
		
		public float TurnSpeed { get { return (float)turnSpeed; } }
		public float MoveSpeed { get { return (float)moveSpeed; } }
		public bool UseAi { get { return useAi; } }
		
	}	
}


