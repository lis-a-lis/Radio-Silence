using UnityEngine;

namespace RadioSilence.Services.InputServices
{
    public class KeyboardInputService : IInputService
    {
        private bool _isInGame = true;

        private const string MOUSE_X = "Mouse X";
        private const string MOUSE_Y = "Mouse Y";

        private const string MOVE_HORIZONTAL = "Horizontal";
        private const string MOVE_VERTICAL = "Vertical";

        public bool Inventory
        {
            get
            {
                _isInGame = !IsInGame;
                return Input.GetKeyDown(KeyCode.Tab);
            }
        }

        public bool Fire => Input.GetMouseButtonDown(0);

        public bool Interact => Input.GetKeyDown(KeyCode.F);

        public bool Reload => Input.GetKeyDown(KeyCode.R);

        public Vector2 MouseDelta => new Vector2(Input.GetAxis(MOUSE_X), Input.GetAxis(MOUSE_Y));

        public Vector2 MoveDirection => new Vector2(Input.GetAxis(MOVE_HORIZONTAL), Input.GetAxis(MOVE_VERTICAL));

        public bool IsInGame => _isInGame;
    }
}