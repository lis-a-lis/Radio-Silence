using System;
using UnityEngine;
using StalkerLikeInventory.Data;
using System.Collections.Generic;

namespace StalkerLikeInventory.Core
{
    public class StalkerInventory : IObservableInventory, IInventory
    {
        private readonly Dictionary<Vector2Int, InventorySlot> _slots;
        private Vector2Int _capacity;
        private bool[,] _fill;

        public StalkerInventory(Vector2Int inventoryGridSize)
        {
            _capacity = inventoryGridSize;

            _slots = new Dictionary<Vector2Int, InventorySlot>();

            _fill = new bool[_capacity.y, _capacity.x];
        }

        public event Action<Vector2Int> InventoryCapacityChanged;
        public event Action<float> ItemsMassChanged;
        public event Action<Vector2Int, ReadOnlyItemData> ItemChanged;
        public event Action<Vector2Int, ReadOnlyItemData> ItemAdded;
        public event Action<Vector2Int> ItemRemoved;

        public Vector2Int Capacity => _capacity;

        public float ItemsMass
        {
            get
            {
                float mass = 0;
                foreach (var slot in _slots)
                    mass += slot.Value.Mass;
                return mass;
            }
        }

        public void GetSlots(out ReadOnlyItemData[] items, out Vector2Int[] positions)
        {
            items = new ReadOnlyItemData[_slots.Count];
            positions = new Vector2Int[_slots.Count];

            int index = 0;

            foreach (var slot in _slots)
            {
                items[index] = slot.Value.ReadOnlyData;
                positions[index] = slot.Key;
                index++;
            }
        }

        public bool HasItem(string itemID, int amount)
        {
            int amountOfItemInInventory = 0;

            foreach (var slot in _slots)
            {
                if (slot.Value.ItemID == itemID)
                {
                    amountOfItemInInventory += slot.Value.Amount;

                    if (amountOfItemInInventory >= amount)
                        return true;
                }
            }
            return false;
        }

        public AddItemsToInventoryResult AddItem(ReadOnlyItemData item)
        {
            if (item.isStackable)
                return AddStackableItem(item, item.amount);
            else if (TryGetEmptySlotPosition(item.size, out Vector2Int position))
            {
                AddItemToNewSlot(position, item);

                ItemAdded?.Invoke(position, item);

                ItemsMassChanged?.Invoke(ItemsMass);

                return new AddItemsToInventoryResult(item.amount, item.amount);
            }

            return new AddItemsToInventoryResult(item.amount, 0);
        }

        public AddItemsToInventoryResult AddItem(ReadOnlyItemData item, Vector2Int position)
        {
            if (!IsEmptySlotExistAtPosition(position.x, position.y, item.size))
                return new AddItemsToInventoryResult(item.amount, 0);

            AddItemToNewSlot(position, item);

            ItemAdded?.Invoke(position, item);

            ItemsMassChanged?.Invoke(ItemsMass);

            return new AddItemsToInventoryResult(item.amount, item.amount);
        }

        public RemoveItemsFromInventoryResult RemoveItem(string itemID, int amount = 1)
        {
            int remainAmount = amount;
            int removedAmount;

            while (remainAmount > 0 && TryGetAvailableToRemoveSlotPosition(itemID, out Vector2Int position))
            {
                removedAmount = Mathf.Clamp(_slots[position].Amount, 0, remainAmount);
                _slots[position].Amount -= removedAmount;
                remainAmount -= removedAmount;

                if (_slots[position].Amount == 0)
                {
                    ItemRemoved?.Invoke(position);
                    _slots.Remove(position);
                }
                else
                    ItemChanged?.Invoke(position, _slots[position].ReadOnlyData);
            }

            ItemsMassChanged?.Invoke(ItemsMass);

            return new RemoveItemsFromInventoryResult(remainAmount != amount, amount, amount - remainAmount);
        }

        public RemoveItemsFromInventoryResult RemoveItem(Vector2Int position, string itemID, int amount = 1)
        {
            if (!_slots.ContainsKey(position))
                return new RemoveItemsFromInventoryResult(false, amount, 0);

            if (_slots[position].ItemID != itemID)
                return new RemoveItemsFromInventoryResult(false, amount, 0);

            int removedItemsAmount;

            if (amount >= _slots[position].Amount)
            {
                removedItemsAmount = _slots[position].Amount;
                ClearSlot(position, _slots[position].Size);
                _slots.Remove(position);
            }
            else
            {
                _slots[position].Amount -= amount;
                removedItemsAmount = amount;
            }

            ItemRemoved?.Invoke(position);

            ItemsMassChanged?.Invoke(ItemsMass);

            return new RemoveItemsFromInventoryResult(true, amount, removedItemsAmount);
        }

        public void ExpandInventory(int newInventoryDepth)
        {
            bool[,] newFill = new bool[newInventoryDepth, _capacity.x];

            for (int y = 0; y < newInventoryDepth; y++)
            {
                for (int x = 0; x < newInventoryDepth; x++)
                    newFill[y, x] = _fill.GetLength(1) <= newInventoryDepth ? _fill[y, x] : false;
            }

            _fill = newFill;
            _capacity = new Vector2Int(_capacity.x, newInventoryDepth);

            InventoryCapacityChanged?.Invoke(_capacity);
        }

        private AddItemsToInventoryResult AddStackableItem(ReadOnlyItemData item, int amount)
        {
            int remainAmount = amount;
            int amountToAdd;

            while (TryGetAvailableToAddExistSlotPosition(item.itemID, out Vector2Int position) && remainAmount > 0)
            {
                amountToAdd = Mathf.Clamp(_slots[position].AvailablePlace, 0, remainAmount);
                _slots[position].Amount += amountToAdd;
                remainAmount -= amountToAdd;

                ItemChanged?.Invoke(position, _slots[position].ReadOnlyData);
            }

            while (remainAmount > 0 && TryGetEmptySlotPosition(item.size, out Vector2Int position))
            {
                amountToAdd = Mathf.Clamp(item.stackSize, 0, remainAmount);

                AddItemToNewSlot(position, new ReadOnlyItemData(item, amountToAdd));

                ItemAdded?.Invoke(position, _slots[position].ReadOnlyData);

                remainAmount -= amountToAdd;
            }

            ItemsMassChanged?.Invoke(ItemsMass);

            return new AddItemsToInventoryResult(amount, amount - remainAmount);
        }

        private void AddItemToNewSlot(Vector2Int position, ReadOnlyItemData item)
        {
            _slots.Add(position, new InventorySlot(item));
            FillSlot(position, item.size);
            ItemAdded?.Invoke(position, item);
        }

        private bool TryGetEmptySlotPosition(Vector2Int itemSize, out Vector2Int position)
        {
            position = default;

            for (int y = 0; y < _capacity.y; y++)
            {
                for (int x = 0; x < _capacity.x; x++)
                {
                    if (IsFilled(x, y))
                        continue;

                    if (IsEmptySlotExistAtPosition(x, y, itemSize))
                    {
                        position = new Vector2Int(x, y);
                        return true;
                    }
                }
            }

            return false;
        }

        private bool TryGetAvailableToAddExistSlotPosition(string itemID, out Vector2Int position)
        {
            position = default;

            foreach (var slot in _slots)
            {
                if (slot.Value.ItemID == itemID && slot.Value.IsAvailableToAdd)
                {
                    position = slot.Key;
                    return true;
                }
            }

            return false;
        }

        private bool TryGetAvailableToRemoveSlotPosition(string itemName, out Vector2Int position)
        {
            position = default;

            foreach (var slot in _slots)
            {
                if (slot.Value.ItemID == itemName)
                {
                    position = slot.Key;
                    return true;
                }
            }

            return false;
        }

        private bool IsEmptySlotExistAtPosition(int xPosition, int yPosition, Vector2Int size)
        {
            int maxXPosition = xPosition + size.x;
            int maxYPosition = yPosition + size.y;

            if (maxXPosition > _capacity.x || maxYPosition > _capacity.y)
                return false;

            for (int y = yPosition; y < maxYPosition; y++)
            {
                for (int x = xPosition; x < maxXPosition; x++)
                {
                    if (IsFilled(x, y))
                        return false;
                }
            }

            return true;
        }

        private bool IsFilled(int x, int y) =>
            _fill[y, x] == true;

        private void FillSlot(Vector2Int position, Vector2Int size) =>
            SetSlotFill(position, size, true);

        private void ClearSlot(Vector2Int position, Vector2Int size) =>
            SetSlotFill(position, size, false);

        private void SetSlotFill(Vector2Int position, Vector2Int size, bool value)
        {
            int maxXPosition = position.x + size.x;
            int maxYPosition = position.y + size.y;

            for (int y = position.y; y < maxYPosition; y++)
            {
                for (int x = position.x; x < maxXPosition; x++)
                    _fill[y, x] = value;
            }
        }
    }
}