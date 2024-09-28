using System;
using UnityEngine;
using System.Collections.Generic;

namespace RadioSilence.InventorySystem.Core
{
    public class Inventory
    {
        private readonly List<InventoryItem> _items = new List<InventoryItem>();

        public void AddItems(string itemID, int amount, bool isStackable, int stackSize)
        {
            if (amount <= 0)
                throw new ArgumentOutOfRangeException();

            if (isStackable)
                AddItemsToStacks(itemID, amount, isStackable, stackSize);
            else
            {
                for (int i = 0; i < amount; i++)
                    AddInventoryItem(itemID, 1, isStackable, stackSize);
            }
        }

        public void RemoveItems(string itemID, int itemIndex, int amount)
        {
            if (_items[itemIndex].ItemID == itemID)
                _items[itemIndex].Amount -= amount;

            if (_items[itemIndex].Amount == 0)
                _items.RemoveAt(itemIndex);
        }

        private void AddItemsToStacks(string itemID, int amount, bool isStackable, int stackSize)
        {
            InventoryItem[] availableStacks = GetAvailableStacks(itemID);
            int addableAmount = stackSize;

            if (availableStacks.Length > 0)
            {
                foreach (InventoryItem item in availableStacks)
                {
                    addableAmount = Mathf.Clamp(amount, 0, stackSize);
                    item.Amount += addableAmount;
                    amount -= addableAmount;

                    if (amount == 0)
                        return;
                }
            }

            while (amount > 0)
            {
                addableAmount = Mathf.Clamp(amount, 0, stackSize);
                amount -= addableAmount;
                AddInventoryItem(itemID, addableAmount, isStackable, stackSize);
            }
        }

        private InventoryItem[] GetAvailableStacks(string itemID)
        {
            List<InventoryItem> stacks = new List<InventoryItem>();

            foreach (InventoryItem item in _items)
            {
                if (item.ItemID == itemID && item.Amount < item.StackSize)
                    stacks.Add(item);
            }

            return stacks.ToArray();
        }

        private void AddInventoryItem(string itemID, int amount, bool isStackable, int stackSize)
        {
            _items.Add(new InventoryItem(itemID, amount, isStackable, stackSize));
        }

        public IReadOnlyInventoryItem[] GetItems()
        {
            IReadOnlyInventoryItem[] readOnlySlots = new IReadOnlyInventoryItem[_items.Count];

            for (int i = 0; i < _items.Count; i++)
                readOnlySlots[i] = _items[i];

            return readOnlySlots;
        }
    }
}