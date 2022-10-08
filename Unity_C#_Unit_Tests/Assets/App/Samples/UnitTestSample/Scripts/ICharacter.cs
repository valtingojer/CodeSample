namespace App.Samples.UnitTestSample
{
	public interface ICharacter
	{
		Inventory Inventory { get; }
		int Health { get; }
		int Level { get; }

        void OnItemEquipped(Item item);
    }
}