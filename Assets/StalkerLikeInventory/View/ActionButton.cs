using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace StalkerLikeInventory.View
{
    [RequireComponent(typeof(Button))]
    public class ActionButton : MonoBehaviour
    {
        private Button _button;
        private TextMeshProUGUI _text;

        public Button Button => _button; 

        private void Awake()
        {
            _button = GetComponent<Button>();
            _text = GetComponentInChildren<TextMeshProUGUI>();
        }
    }
}