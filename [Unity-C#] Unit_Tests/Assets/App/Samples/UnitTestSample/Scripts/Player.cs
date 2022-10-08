using UnityEngine;

namespace App.Samples.UnitTestSample
{
    [RequireComponent(typeof(Rigidbody))]
    public class Player : MonoBehaviour
    {
        private Rigidbody _rigidbody;
        private float moveSpeed = 100f;
        public IPlayerInput PlayerInput { get; private set; }

        public void AssignInput(IPlayerInput playerInput)
        {
            PlayerInput = playerInput;
        }

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _rigidbody.useGravity = false;
        }
        private void Update()
        {
            float vertical = PlayerInput.Vertical;
            float verticalModifier = vertical * moveSpeed * Time.deltaTime;
            var verticalForce = new Vector3(0, 0, verticalModifier);
            _rigidbody.AddForce(verticalForce);
        }
    }
}


