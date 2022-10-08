using System;
using System.Collections;
using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace App.Samples.UnitTestSample
{
    public class inventory
    {
        [Test]
	    public void inventory_only_allows_one_chest_eqquiped_at_time()
        {
	        //Arrange
	        var slot = EquipmentSlots.Chest;
            var character = Substitute.For<ICharacter>();
            Inventory inventory = new Inventory(character);
	        Item chestOne = new Item() {EquipmentSlot = slot, Id = Guid.NewGuid() };
	        Item chestTwo = new Item() {EquipmentSlot = slot, Id = Guid.NewGuid() };

            character.Inventory.Returns(inventory);

            //Act
            inventory.EquipItem(chestOne);
	        inventory.EquipItem(chestTwo);
	        
	        //Assert
	        var equipped = inventory.GetItem(slot);
	        Assert.AreSame(chestTwo, equipped);
	        Assert.AreEqual(chestTwo, equipped);
	        Assert.AreEqual(chestTwo.Id, equipped.Id);
        }

        [Test]
        public void tells_character_when_an_item_is_equipped_successfully()
        {
            //Arrange
            var character = Substitute.For<ICharacter>();
            Inventory inventory = new Inventory(character);
            Item chest = new Item() { EquipmentSlot = EquipmentSlots.Chest, Id = Guid.NewGuid() };
            character.Inventory.Returns(inventory);

            //Act
            inventory.EquipItem(chest);

            //Assert
            character.Received().OnItemEquipped(chest);
        }
    }
}
