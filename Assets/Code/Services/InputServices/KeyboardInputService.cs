using UnityEngine;

namespace RadioSilence.Services.InputServices
{
    public class KeyboardInputService : IInputService
    {
        private bool _isInUi = false;

        private const string MOUSE_X = "Mouse X";
        private const string MOUSE_Y = "Mouse Y";

        private const string MOVE_HORIZONTAL = "Horizontal";
        private const string MOVE_VERTICAL = "Vertical";

        public bool IsInUI => _isInUi;

        public bool Inventory
        {
            get
            {
                _isInUi = !IsInUI;
                return Input.GetKeyDown(KeyCode.Tab);
            }
        }

        public bool Fire => Input.GetMouseButtonDown(0);

        public bool Interact => Input.GetKeyDown(KeyCode.F);

        public bool Reload => Input.GetKeyDown(KeyCode.R);

        public Vector2 MouseDelta => new Vector2(Input.GetAxis(MOUSE_X), Input.GetAxis(MOUSE_Y));

        public Vector2 MoveDirection => new Vector2(Input.GetAxis(MOVE_HORIZONTAL), Input.GetAxis(MOVE_VERTICAL));
    }
}