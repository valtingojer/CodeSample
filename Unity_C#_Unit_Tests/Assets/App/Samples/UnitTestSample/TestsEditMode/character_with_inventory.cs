using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using NSubstitute;

namespace App.Samples.UnitTestSample
{
    public class character_with_inventory
    {
        [Test]
	    public void with_90_armor_takes_10_percent_damage()
        {
	        //Arrange
	        ICharacter character = Substitute.For<ICharacter>();
	        Inventory inventory = new Inventory(character);
	        Item pants = new Item(){ Id = Guid.NewGuid(), EquipmentSlot = EquipmentSlots.Legs, Armor = 40 };
	        Item shield = new Item(){ Id = Guid.NewGuid(), EquipmentSlot = EquipmentSlots.RightHand, Armor = 50 };
	        
	        inventory.EquipItem(pants);
	        inventory.EquipItem(shield);
	        character.Inventory.Returns(inventory);
	        
	        //Act
	        var mitigation = DamageCalculator.CalculateDamage(1000, character);
	        
	        //Assert
	        Assert.AreEqual(100, mitigation);
        }
    }
}
