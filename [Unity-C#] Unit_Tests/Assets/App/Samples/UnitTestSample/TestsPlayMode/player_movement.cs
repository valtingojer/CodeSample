using System.Collections;
using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace App.Samples.UnitTestSample
{
    public class player_movement
    {
        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator with_positive_vertical_input_moves_forward()
        {
            GameObject playerGameObject = new GameObject("Player");
            playerGameObject.AddComponent<Rigidbody>();
            Player player = playerGameObject.AddComponent<Player>();
            player.AssignInput(Substitute.For<IPlayerInput>());
            player.PlayerInput.Vertical.Returns(1f);

            playerGameObject.transform.position = Vector3.zero;

            var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

            cube.transform.SetParent(playerGameObject.transform);
            cube.transform.localPosition = Vector3.zero;


            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return new WaitForSeconds(3f);
            
            Assert.AreEqual(0f, playerGameObject.transform.position.x, $"Player position x is {playerGameObject.transform.position.x} instead of 0");
            Assert.IsTrue(playerGameObject.transform.position.y == 0f, $"Player position y is {playerGameObject.transform.position.y} instead of 0");
            Assert.Greater(playerGameObject.transform.position.z, 0f, $"Player position z is {playerGameObject.transform.position.z} instead of greater than 0");
        }
    }
}
