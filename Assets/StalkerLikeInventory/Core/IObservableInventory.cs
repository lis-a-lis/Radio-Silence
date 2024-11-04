using System;
using UnityEngine;
using StalkerLikeInventory.Data;

namespace StalkerLikeInventory.Core
{
    public interface IObservableInventory
    {
        public Vector2Int Capacity { get; }
        public float ItemsMass { get; }

        public event Action<Vector2Int> InventoryCapacityChanged;
        public event Action<float> ItemsMassChanged;
        public event Action<Vector2Int, ReadOnlyItemData> ItemChanged;
        public event Action<Vector2Int, ReadOnlyItemData> ItemAdded;
        public event Action<Vector2Int> ItemRemoved;

        public void GetSlots(out ReadOnlyItemData[] items, out Vector2Int[] positions);
    }
}