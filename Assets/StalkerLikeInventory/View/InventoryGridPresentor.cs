using UnityEngine;
using StalkerLikeInventory.Data;
using StalkerLikeInventory.Core;

namespace StalkerLikeInventory.View
{
    // IObservableInventory > PlayerInventoryPresenter > InventoryGridView
    public class InventoryGridPresentor
    {
        private IInventoryGridView _inventoryGridView;
        private IObservableInventory _observableInventory;
        private IActionsWithItemInInventoryView _actionsView;

        public InventoryGridPresentor(IObservableInventory observableInventory, IInventoryGridView inventoryGridView)
        {
            _observableInventory = observableInventory;
            _inventoryGridView = inventoryGridView;
        }

        public void Enable()
        {
            _observableInventory.ItemAdded += OnItemAdded;
            _observableInventory.ItemRemoved += OnItemRemoved;
            _observableInventory.ItemChanged += OnItemChanged;
            _observableInventory.InventoryCapacityChanged += OnInventoryCapacityChanged;

            _inventoryGridView.SlotSelected += OnSlotSelected;
        }

        private void OnSlotSelected(Vector2Int slotPosition)
        {
            Debug.Log($"slot at position: {slotPosition} selected");
        }

        public void Disable()
        {
            _observableInventory.ItemAdded -= OnItemAdded;
            _observableInventory.ItemRemoved -= OnItemRemoved;
            _observableInventory.ItemChanged -= OnItemChanged;
            _observableInventory.InventoryCapacityChanged -= OnInventoryCapacityChanged;

            _inventoryGridView.SlotSelected -= OnSlotSelected;
        }

        private void OnItemAdded(Vector2Int position, ReadOnlyItemData item)
        {
            _inventoryGridView.AddItem(position, item);
        }

        private void OnItemRemoved(Vector2Int position)
        {
            _inventoryGridView.RemoveItem(position);
        }

        private void OnItemChanged(Vector2Int position, ReadOnlyItemData item)
        {
            _inventoryGridView.ChangeItem(position, item);
        }

        private void OnInventoryCapacityChanged(Vector2Int capacity)
        {
            _inventoryGridView.ChangeGridSize(capacity);
        }
    }
}