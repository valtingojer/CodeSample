using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.Samples.UnitTestSample
{
	public class Item
	{
		public Guid Id { get; set; }
		public EquipmentSlots EquipmentSlot { get; set; }
		public int Armor { get; set; }
	}
}
