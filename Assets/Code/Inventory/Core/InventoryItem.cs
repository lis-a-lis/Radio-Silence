using System;

namespace RadioSilence.InventorySystem.Core
{
    public class InventoryItem : IReadOnlyInventoryItem
    {
        private int _amount = 0;

        public string ItemID { get; }

        public int Amount
        {
            get => _amount;
            set
            {
                _amount = value;
            }
        }


        public bool IsEmpty => Amount == 0;

        public bool IsStackable { get; }

        public float Mass { get; }

        public int StackSize { get; }

        public InventoryItem(string itemID, int amount, bool isStackable, int stackSize, float mass)
        {
            ItemID = itemID;
            Amount = amount;
            IsStackable = isStackable;
            StackSize = stackSize;
            Mass = mass;
        }
    }
}