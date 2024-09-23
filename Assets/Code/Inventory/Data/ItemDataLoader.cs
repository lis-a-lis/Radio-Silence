using RadioSilence.InventorySystem.Core;
using System.Collections.Generic;
using UnityEngine;

namespace RadioSilence.InventorySystem.Data
{
    public class ItemDataLoader
    {
        private const string DataPath = "ItemsData/";
        private const string PrefabsPath = "ItemPrefabs/";

        private readonly Dictionary<string, ItemData> _itemsData;
        private readonly Dictionary<string, GameObject> _itemPrefabs;

        private static ItemDataLoader _instance;

        public static ItemDataLoader Instance
        {
            get
            {
                _instance ??= new ItemDataLoader();

                return _instance;
            }
        }

        public ItemDataLoader()
        {
            _itemsData = new Dictionary<string, ItemData>();
            _itemPrefabs = new Dictionary<string, GameObject>();
        }

        public GameObject LoadItemPrefab(string itemID)
        {
            if (_itemPrefabs.ContainsKey(itemID))
                return _itemPrefabs[itemID];

            GameObject gameObject = Resources.Load<GameObject>(PrefabsPath + itemID);
            _itemPrefabs.Add(itemID, gameObject);

            return gameObject;
        }

        public ItemData LoadItemData(string itemID)
        {
            if (_itemsData.ContainsKey(itemID))
                return _itemsData[itemID];

            ItemData itemData = Resources.Load<ItemData>(DataPath + itemID);
            _itemsData.Add(itemID, itemData);

            return itemData;
        }

        public ReadOnlyItemData LoadReadOnlyItemData(IReadOnlyInventoryItem item)
        {
            ItemData currentItemData = LoadItemData(item.ItemID);

            return new ReadOnlyItemData(
                currentItemData.ItemID,
                currentItemData.ItemName,
                currentItemData.Description,
                item.Amount,
                currentItemData.StackSize,
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
        }
    }
}