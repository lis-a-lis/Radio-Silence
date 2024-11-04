using UnityEngine;

namespace StalkerLikeInventory.Data
{
    public interface IReadOnlyItemData
    {
        public string ItemID { get; }
        public int Amount { get; }
        public bool IsStackable { get; }
        public int StackSize { get; }
        public Vector2Int Size { get; }
        public float Strength { get; }
        public float Mass { get; }
    }

    public readonly struct ReadOnlyItemData
    {
        public readonly string itemID;
        public readonly Vector2Int size;
        public readonly bool isStackable;
        public readonly int stackSize;
        public readonly float strength;
        public readonly float mass;
        public readonly int amount;

        public ReadOnlyItemData(string itemID, int amount, float mass, float strength, Vector2Int size, bool isStackable, int stackSize)
        {
            this.itemID = itemID;
            this.amount = amount;
            this.mass = mass;
            this.strength = strength;
            this.size = size;
            this.isStackable = isStackable;
            this.stackSize = stackSize;
        }

        public ReadOnlyItemData(ItemData data, int amount)
        {
            this.amount = amount;
            itemID = data.ItemID;
            mass = data.Mass;
            strength = data.Strength;
            size = data.Size;
            isStackable = data.IsStackable;
           stackSize = data.StackSize;
        }

        public ReadOnlyItemData(ReadOnlyItemData sourceItemData, int newItemAmount)
        {
            amount = newItemAmount;
            itemID = sourceItemData.itemID;
            mass = sourceItemData.mass;
            strength = sourceItemData.strength;
            size = sourceItemData.size;
            isStackable = sourceItemData.isStackable;
            stackSize = sourceItemData.stackSize;
        }
    }
}