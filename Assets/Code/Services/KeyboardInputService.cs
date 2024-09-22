using UnityEngine;

namespace RadioSilence.Services
{
    public class KeyboardInputService : IInputService
    {
        private string _mouseX = "Mouse X";
        private string _mouseY = "Mouse Y";

        private string _moveHorizontal = "Horizontal";
        private string _moveVertical = "Vertical";

        public Vector2 MouseDelta => new Vector2(Input.GetAxis(_mouseX), Input.GetAxis(_mouseY));

        public Vector2 MoveDirection => new Vector2(Input.GetAxis(_moveHorizontal), Input.GetAxis(_moveVertical));

        public bool Inventory => Input.GetKeyDown(KeyCode.Tab);

        public bool Fire => Input.GetMouseButtonDown(0);

        public bool Interact => Input.GetKeyDown(KeyCode.F);

        public bool Reload => Input.GetKeyDown(KeyCode.R);
    }
}