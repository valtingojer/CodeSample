using UnityEngine;

namespace App.Samples.UnitTestSample
{
    public class PlayerInput : IPlayerInput
    {
        public float Vertical => Input.GetAxis("Vertical");
    }
}


