using System;
namespace App.Samples.DataModelHandler.Scripts
{
	[Serializable]
	public class PlayerModel
	{
		public int Health { get; set; }
		public int Mana { get; set; }
		public int MovimentSpeed { get; set; }
	}	
}