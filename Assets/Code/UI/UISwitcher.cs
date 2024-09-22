using UnityEngine;

namespace RadioSilence.UI
{
    public class UISwitch : MonoBehaviour
    {
        [SerializeField] private RectTransform _playerInventoryUI;

        private void Awake()
        {
            HideCursor();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                _playerInventoryUI.gameObject.SetActive(!_playerInventoryUI.gameObject.activeSelf);

                if (_playerInventoryUI.gameObject.activeSelf == false)
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