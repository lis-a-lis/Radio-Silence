using System.Collections.Generic;
using UnityEngine;

namespace StalkerLikeInventory.Data
{
    public sealed class ItemLoader
    {
        private const string PREFABS_PATH = "ItemPrefabs/";
        private readonly Dictionary<string, GameObject> _itemPrefabs = new Dictionary<string, GameObject>();

        private static ItemLoader _instance;

        public static ItemLoader Instance => _instance ??= new ItemLoader();

        public GameObject LoadItemPrefab(string resourceName)
        {
            if (_itemPrefabs.ContainsKey(resourceName))
                return _itemPrefabs[resourceName];

            GameObject gameObject = Resources.Load<GameObject>(PREFABS_PATH + resourceName);
            _itemPrefabs.Add(resourceName, gameObject);

            return gameObject;
        }

        public ItemData LoadItemData(string resourceName)
        {
            ItemData itemData = Resources.Load<ItemData>(resourceName);

            return itemData;
        }

/*        public ReadOnlyItemData LoadReadOnlyItemData(IReadOnlyInventoryItem item)
        {
            ItemData currentItemData = LoadItemData(item.ItemID);

            return new ReadOnlyItemData(
                currentItemData.ResourceName,
                currentItemData.ItemName,
                currentItemData.Description,
                item.Amount,
                currentItemData.StackSize,
                currentItemData.Mass,
                currentItemData.Icon,
                currentItemData.Actions
            );
        }

        public ReadOnlyItemData[] LoadReadOnlyItemsDataArray(IReadOnlyInventoryItem[] items)
        {
            ReadOnlyItemData[] data = new ReadOnlyItemData[items.Length];

            for (int i = 0; i < items.Length; i++)
                data[i] = LoadReadOnlyItemData(items[i]);

            return data;
        }*/
    }
}