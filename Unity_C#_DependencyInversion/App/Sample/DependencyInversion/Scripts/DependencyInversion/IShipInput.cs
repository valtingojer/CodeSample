namespace App.Samples.DependencyInversion.Scripts
{
	public interface IShipInput
	{
		void ReadInput();
		float Rotation { get; }
		float Thrust { get; }
	}
}