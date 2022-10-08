using UnityEngine;

namespace App.Samples.DependencyInversion.Scripts
{
	public class ShipMotor
	{
		private readonly IShipInput shipInput;
		private readonly Transform transformToMove;
		private readonly ShipSettings shipSettings;
		
		private Vector3 turnDelta => Vector3.up * shipInput.Rotation * shipSettings.TurnSpeed * Time.deltaTime;
		private Vector3 moveDelta => transformToMove.forward * shipInput.Thrust * shipSettings.MoveSpeed * Time.deltaTime;
		
		public ShipMotor(IShipInput shipInput, Transform transformToMove, ShipSettings shipSettings)
		{
			this.shipInput = shipInput;
			this.transformToMove = transformToMove;
			this.shipSettings = shipSettings;
		}
		
		public void Tick()
		{
			transformToMove.Rotate(turnDelta);
			transformToMove.position += moveDelta;
		}
	}	
}
