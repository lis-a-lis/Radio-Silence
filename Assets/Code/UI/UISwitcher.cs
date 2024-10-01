using RadioSilence.Services.InputServices;
using UnityEngine;

namespace RadioSilence.UI
{
    public class UISwitcher : MonoBehaviour
    {
        private GameObject _playerInventoryUI;
        private IInputService _input;

        public void Initialize(GameObject playerInventory)
        {
            _playerInventoryUI = playerInventory;
        }

        public void Inject(IInputService input)
        {
            _input = input;
        }

        private void Awake()
        {
            HideCursor();
        }

        private void Update()
        {
            if (_input.Inventory)
            {
                _playerInventoryUI.SetActive(!_playerInventoryUI.activeSelf);

                if (_playerInventoryUI.activeSelf == false)
                    HideCursor();
                else
                    ShowCursor();
            }
        }

        private void HideCursor()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void ShowCursor()
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
    }
}