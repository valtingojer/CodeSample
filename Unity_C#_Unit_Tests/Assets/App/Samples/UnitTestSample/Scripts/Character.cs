using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.Samples.UnitTestSample
{
	public class Character : MonoBehaviour, ICharacter
	{
		public Inventory Inventory { get; set; }
		public int Health { get; set; }
		public int Level { get; set; }

        public void OnItemEquipped(Item item)
        {
            Debug.Log($"Equipped {item.EquipmentSlot} with armor {item.Armor} and id {item.Id}");
        }
    }
}