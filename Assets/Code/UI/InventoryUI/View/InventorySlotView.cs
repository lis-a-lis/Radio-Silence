using TMPro;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using RadioSilence.InventorySystem.Data;

namespace RadioSilence.UI.InventoryUI.View
{
    public class InventorySlotView : MonoBehaviour, IPointerClickHandler
    {
        private readonly Color _defaultIconColor = new Color(1, 1, 1, 0);
        private ReadOnlyItemData _data;

        [SerializeField] private TextMeshProUGUI _amount;
        [SerializeField] private Image _icon;

        public event Action<InventorySlotView, ReadOnlyItemData> OnClicked;

        private bool IsClear => _icon.sprite == null;

        public void SetView(ReadOnlyItemData data)
        {
            _data = data;
            _icon.sprite = data.icon;
            _icon.color = Color.white;
            _amount.text = data.IsNotSingle ? data.amount.ToString() : string.Empty;
        }

        public void Clear()
        {
            _icon.sprite = null;
            _amount.text = string.Empty;
            _icon.color = _defaultIconColor;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (!IsClear)
                OnClicked?.Invoke(this, _data);
        }
    }
}
