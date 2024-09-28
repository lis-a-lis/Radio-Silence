using UnityEngine;

namespace RadioSilence.UI
{
    public class UISwitcher : MonoBehaviour
    {
        private GameObject _playerInventoryUI;

        public void InitializeUISwitcher(GameObject playerInventory)
        {
            _playerInventoryUI = playerInventory;
        }

        private void Awake()
        {
            HideCursor();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Tab))
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