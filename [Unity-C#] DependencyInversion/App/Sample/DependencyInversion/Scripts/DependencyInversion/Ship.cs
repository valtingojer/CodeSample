using UnityEngine;

namespace App.Samples.DependencyInversion.Scripts
{
	public class Ship : MonoBehaviour
	{
		[SerializeField]
		private ShipSettings shipSettings;
		
		private IShipInput shipInput;
		private ShipMotor shipMotor;
		
		// Awake is called when the script instance is being loaded.
		protected void Awake()
		{
			shipInput = shipSettings.UseAi ? new AiInput() as IShipInput : new ControllerInput();
			shipMotor = new ShipMotor(shipInput, transform, shipSettings);
		}
		
		// Update is called every frame, if the MonoBehaviour is enabled.
		protected void Update()
		{
			shipInput.ReadInput();
			shipMotor.Tick();
		}
	}	
}


