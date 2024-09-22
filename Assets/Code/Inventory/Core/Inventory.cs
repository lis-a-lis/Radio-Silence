using System;
using System.Collections.Generic;
using UnityEngine;

namespace RadioSilence.InventorySystem.Core
{
    public readonly struct ReadOnlyItem
    {
        public readonly string itemID;
        public readonly int amount;
        public readonly bool isStackable;
        public readonly int stackSize;
        public readonly float mass;

        public ReadOnlyItem(string itemID, int amount, bool isStackable, int stackSize, float mass)
        {
            this.itemID = itemID;
            this.amount = amount;
            this.isStackable = isStackable;
            this.stackSize = stackSize;
            this.mass = mass;
        }
    }

    public class Inventory
    {
        private readonly List<InventoryItem> _items = new List<InventoryItem>();

        public event Action OnInventoryChanged;

        public float Mass => CalculateItemsMass();

        public void AddItems(string itemID, int amount, bool isStackable, int stackSize, float mass)
        {
            if (amount <= 0)
                throw new ArgumentOutOfRangeException();

            if (isStackable)
                AddItemsToStacks(itemID, amount, isStackable, stackSize, mass);
            else
            {
                for (int i = 0; i < amount; i++)
                    AddItem(itemID, 1, isStackable, stackSize, mass);
            }

            OnInventoryChanged?.Invoke();
        }

        public void RemoveItems(string itemID, int itemIndex, int amount)
        {
            if (_items[itemIndex].ItemID == itemID)
                _items[itemIndex].Amount -= amount;

            if (_items[itemIndex].Amount == 0)
                _items.RemoveAt(itemIndex);

            OnInventoryChanged?.Invoke();
        }

        private void AddItemsToStacks(string itemID, int amount, bool isStackable, int stackSize, float mass)
        {
            InventoryItem[] availableStacks = GetAvailableStacks(itemID);
            int addableAmount = stackSize;
            Debug.Log("ADD_AMOUNT - " + amount);
            Debug.Log("AVAILABLE_STACK - " + availableStacks);
            Debug.Log("AVAILABLE_STACK len - " + availableStacks.Length);
            if (availableStacks.Length > 0)
            {
                Debug.Log("ADDING TO AVAILABLE STACK");
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
                AddItem(itemID, addableAmount, isStackable, stackSize, mass);

                Debug.Log($"ADDDDDD {addableAmount} { amount}");
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

        private void AddItem(string itemID, int amount, bool isStackable, int stackSize, float mass)
        {
            _items.Add(new InventoryItem(itemID, amount, isStackable, stackSize, mass));
        }

        public IReadOnlyInventoryItem[] GetItems()
        {
            IReadOnlyInventoryItem[] readOnlySlots = new IReadOnlyInventoryItem[_items.Count];

            for (int i = 0; i < _items.Count; i++)
                readOnlySlots[i] = _items[i];

            return readOnlySlots;
        }

        private float CalculateItemsMass()
        {
            float mass = 0f;

            foreach (InventoryItem item in _items)
                mass += item.Mass;

            return mass;
        }
    }
}