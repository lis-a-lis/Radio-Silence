using TMPro;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using RadioSilence.InventorySystem.Data;

namespace RadioSilence.UI.InventoryUI
{
    [RequireComponent(typeof(Image))]
    public class InventorySlotView : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private TextMeshProUGUI _amount;
        [SerializeField] private Image _icon;

        private ReadOnlyInventoryItemData _data;

        public event Action<InventorySlotView, ReadOnlyInventoryItemData> OnClicked;

        public bool IsClear => _icon.sprite == null;

        public void SetView(ReadOnlyInventoryItemData data)
        {
            _data = data;
            _icon.sprite = data.icon;
            _icon.color = Color.white;
            if (data.amount > 1)
                _amount.text = data.amount.ToString();
            else
                _amount.text = string.Empty;
        }

        public void Clear()
        {
            _icon.sprite = null;
            _amount.text = string.Empty;
            _icon.color = new Color(1, 1, 1, 0);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (!IsClear)
                OnClicked?.Invoke(this, _data);
        }
    }
}
