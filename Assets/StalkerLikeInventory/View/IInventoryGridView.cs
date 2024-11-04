using System;
using UnityEngine;
using StalkerLikeInventory.Data;

namespace StalkerLikeInventory.View
{
    public interface IInventoryGridView
    {
        public event Action<Vector2Int> SlotSelected;

        public void AddItem(Vector2Int position, ReadOnlyItemData item);
        public void RemoveItem(Vector2Int position);
        public void ChangeItem(Vector2Int position, ReadOnlyItemData item);
        public void ChangeGridSize(Vector2Int size);
    }
}