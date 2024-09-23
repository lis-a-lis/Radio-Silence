using UnityEngine;

namespace RadioSilence
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float _speed = 5;

        private CharacterController _characterController;

        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
        }

        private Vector2 GetInput() =>
            new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        private Vector3 GetMovementDirection()
        {
            Vector2 input = GetInput();
            Vector3 direction = transform.forward * input.y + transform.right * input.x + Physics.gravity;

            return direction;
        }
    
        private void Update()
        {
            _characterController.Move(_speed * Time.deltaTime * GetMovementDirection());
        }
    }
}