using UnityEngine;

namespace RadioSilence
{
    [RequireComponent(typeof(Camera))]
    public class CameraMovement : MonoBehaviour
    {
        [SerializeField] private float _sensitivity = 2;
        [SerializeField] private Vector2 _rotationAngleLimits = new Vector2(-60, 60);

        private float _horizontalRotation;

        private Vector2 GetInput() =>
           new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        private void Update()
        {
            Vector2 input = GetInput();
            transform.parent.Rotate(_sensitivity * input.x * Vector3.up);

            _horizontalRotation -= input.y * _sensitivity;
            _horizontalRotation = Mathf.Clamp(_horizontalRotation, _rotationAngleLimits.x, _rotationAngleLimits.y);
            transform.localRotation = Quaternion.Euler(_horizontalRotation, 0, 0);
        }
    }
}