using TMPro;
using System;
using UnityEngine;
using System.Collections.Generic;
using StalkerLikeInventory.Data;
using StalkerLikeInventory.Core;
using StalkerLikeInventory.Gameplay;

namespace StalkerLikeInventory.View
{
    public enum ActionsWithItem
    {
        Drop,
        Use,
        Reload,
        Unload,
        Repair,
    }

    public interface IActionsWithItemInInventoryView
    {
        public void UpdateView(Vector2 position, ActionsWithItem actionsWithItem);
    }

    public class PlayerInventoryView : MonoBehaviour
    {
        [SerializeField] private int _pixelCellResolution = 80;
        [SerializeField] private int _minPixelGridHeight = 880;
        [SerializeField] private ItemSlotView _itemViewPrefab;
        [SerializeField] private RectTransform _contentTransform;
        [SerializeField] private TextMeshProUGUI _itemsMass;

        private Dictionary<Vector2Int, ItemSlotView> _views = new Dictionary<Vector2Int, ItemSlotView>();
        private IObservableInventory _playerInventory;
        private int _contentRectHeight;

        private void Start()
        {
            _contentRectHeight = _minPixelGridHeight;
            _playerInventory = FindFirstObjectByType<StalkerBackpack>().PlayerInventory;
            _playerInventory.ItemAdded += OnItemAdded;
            _playerInventory.ItemRemoved += OnItemRemoved;
            _playerInventory.ItemChanged += OnItemChanged;
        }

        private void OnDisable()
        {
            if (_playerInventory != null)
            {
                _playerInventory.ItemAdded -= OnItemAdded;
                _playerInventory.ItemRemoved -= OnItemRemoved;
                _playerInventory.ItemChanged -= OnItemChanged;
            }
        }

        private void OnItemChanged(Vector2Int position, ReadOnlyItemData item)
        {
            _views[position].UpdateAmount(item.amount);
            _itemsMass.text = _playerInventory.ItemsMass.ToString();
        }

        private void OnItemRemoved(Vector2Int position)
        {
            Destroy(_views[position].gameObject);
            _views.Remove(position);
        }

        private void OnItemAdded(Vector2Int position, ReadOnlyItemData item)
        {
            ItemSlotView view = Instantiate(_itemViewPrefab, _contentTransform);
            _views[position] = view;
            view.UpdateView(ItemLoader.Instance.LoadItemData(item.itemID).Icon,
                                position, item.size, item.amount);

            ResizeContentRect(position.y + item.size.y);

            _itemsMass.text = Math.Round(_playerInventory.ItemsMass, 2).ToString() + " kg";
        }

        private void ResizeContentRect(int maxItemYPosition)
        {
            if (maxItemYPosition * _pixelCellResolution < _contentRectHeight)
                return;

            _contentRectHeight = Mathf.Clamp(maxItemYPosition * _pixelCellResolution,
                                            _minPixelGridHeight,
                                            _playerInventory.Capacity.y * _pixelCellResolution);

            _contentTransform.sizeDelta = new Vector2(_contentTransform.sizeDelta.x, _contentRectHeight);
        }
    }
}