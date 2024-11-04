using UnityEngine;
using StalkerLikeInventory.Data;

namespace StalkerLikeInventory.Core
{
    public interface IInventory : IObservableInventory
    {
        bool HasItem(string itemID, int amount);
        AddItemsToInventoryResult AddItem(ReadOnlyItemData item);
        AddItemsToInventoryResult AddItem(ReadOnlyItemData item, Vector2Int position);
        RemoveItemsFromInventoryResult RemoveItem(string itemID, int amount = 1);
        RemoveItemsFromInventoryResult RemoveItem(Vector2Int position, string itemID, int amount = 1);
        void ExpandInventory(int newInventoryDepth);
    }
}