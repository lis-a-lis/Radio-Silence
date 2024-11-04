using System;
using UnityEngine;
using System.Collections.Generic;
using StalkerLikeInventory.Data;

namespace StalkerLikeInventory.View
{
    [RequireComponent(typeof(RectTransform))]
    public class InventoryGridView : MonoBehaviour, IInventoryGridView
    {
        [SerializeField] private Vector2Int _gridSize;
        [SerializeField] private ItemSlotView _itemViewPrefab;
        [SerializeField] private RectTransform _contentTransform;

        private Dictionary<Vector2Int, ItemSlotView> _views = new Dictionary<Vector2Int, ItemSlotView>();
        private Vector2 _visibleGridResolution;
        private Vector2 _contentGridResolution;
        private int _gridCellResolution;

        public event Action<Vector2Int> SlotSelected;

        private void Awake()
        {
            _visibleGridResolution = gameObject.GetComponent<RectTransform>().sizeDelta;
            _gridCellResolution = (int)_visibleGridResolution.x / _gridSize.x;
            _contentGridResolution = _visibleGridResolution;
        }

        public void AddItem(Vector2Int position, ReadOnlyItemData item)
        {
            InitializeNewSlotView(position);

            _views[position].UpdateView(ItemLoader.Instance.LoadItemData(item.itemID).Icon,
                                        position, item.size, item.amount);

            ResizeContentGrid(position.y + item.size.y);
        }

        public void RemoveItem(Vector2Int position)
        {
            Destroy(_views[position].gameObject);

            _views.Remove(position);

            ResizeContentGrid(position.y);
        }

        public void ChangeItem(Vector2Int position, ReadOnlyItemData item)
        {
            _views[position].UpdateAmount(item.amount);
        }

        public void ChangeGridSize(Vector2Int size)
        {
            _gridSize = size;
        }

        private void InitializeNewSlotView(Vector2Int position)
        {
            ItemSlotView view = Instantiate(_itemViewPrefab, _contentTransform);
            _views[position] = view;
            view.IsSelected += OnSlotSelected;
        }

        private void OnSlotSelected(Vector2Int slotPosition)
        {
            SlotSelected?.Invoke(slotPosition);
        }

        private void ResizeContentGrid(int verticalPositionOfSlotBottom)
        {
            if (verticalPositionOfSlotBottom * _gridCellResolution < _visibleGridResolution.y)
                return;

            _contentGridResolution.y = Mathf.Clamp(verticalPositionOfSlotBottom * _gridCellResolution,
                                                   _visibleGridResolution.y,
                                                   _gridSize.y * _gridCellResolution);

            _contentTransform.sizeDelta = new Vector2(_contentGridResolution.x, _contentGridResolution.y);
        }
    }
}