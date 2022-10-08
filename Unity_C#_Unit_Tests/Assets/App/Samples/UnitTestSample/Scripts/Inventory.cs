using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace App.Samples.UnitTestSample
{
    public class Inventory
	{
		private readonly ICharacter _character;
		public Inventory(ICharacter character)
		{
			_character = character;
		}

		Dictionary<EquipmentSlots, Item> _equippedItems = new Dictionary<EquipmentSlots, Item>();
		IList<Item> _unequippedItems = new List<Item>();
		
		
		private void UnequippedSlot(EquipmentSlots equipmentSlot)
		{
			if(_equippedItems.ContainsKey(equipmentSlot))
			{
				_unequippedItems.Add(_equippedItems[equipmentSlot]);
				_equippedItems.Remove(equipmentSlot);
			}
		}
		private void EquipSlot(Item item)
		{
			_equippedItems.Add(item.EquipmentSlot, item);
		}
		
		public Item GetItem(EquipmentSlots equipmentSlot)
		{
			Item result = null;
			if(_equippedItems.ContainsKey(equipmentSlot))
				result = _equippedItems[equipmentSlot];
			
			return result;
		}
		
		public void EquipItem(Item item)
		{
			UnequippedSlot(item.EquipmentSlot);
			EquipSlot(item);
			
			_character.OnItemEquipped(item);
		}
		
		public int GetTotalArmor()
		{
			int totalArmor = _equippedItems.Values.Sum(x => x.Armor);
			return totalArmor;
		}
	}
}


