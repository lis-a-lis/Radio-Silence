using System;
using StalkerLikeInventory.Core;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace StalkerLikeInventory.View
{
    [RequireComponent(typeof(Image))]
    public class ItemSlotView : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Image _itemIcon;
        [SerializeField] private TextMeshProUGUI _itemAmountText;

        private Image _backgroundImage;
        private int _pixelCellResolution;

        public event Action<Vector2Int> IsSelected;

        private void Awake()
        {
            _backgroundImage = GetComponent<Image>();
            _pixelCellResolution = (int)_itemIcon.rectTransform.sizeDelta.x;
        }

        public void UpdateView(Sprite icon, Vector2Int position, Vector2Int size, int amount)
        {
            _itemIcon.sprite = icon;
            _itemIcon.color = Color.white;
            _itemIcon.rectTransform.sizeDelta = size * _pixelCellResolution;
            _backgroundImage.rectTransform.anchoredPosition = new Vector3(position.x, -position.y) * _pixelCellResolution;
            _backgroundImage.rectTransform.sizeDelta = _itemIcon.rectTransform.sizeDelta;
            UpdateAmount(amount);
        }

        public void UpdateAmount(int amount)
        {
            _itemAmountText.text = amount == 1 ? string.Empty : amount.ToString();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            Vector2Int position = new Vector2Int(
                (int)_backgroundImage.rectTransform.anchoredPosition.x,
                (int)-_backgroundImage.rectTransform.anchoredPosition.y);

            IsSelected?.Invoke(position);
        }
    }
}