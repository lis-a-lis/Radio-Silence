using RadioSilence.InventorySystem.Core;
using System.Collections.Generic;
using UnityEngine;

namespace RadioSilence.InventorySystem.Data
{
    public class ItemsDataLoader
    {
        private const string ItemsDataPath = "ItemsData/";

        private static ItemsDataLoader _instance;

        private readonly Dictionary<string, ItemData> _itemsData;

        public static ItemsDataLoader Instance
        {
            get
            {
                _instance ??= new ItemsDataLoader();

                return _instance;
            }
        }

        public ItemsDataLoader()
        {
            _itemsData = new Dictionary<string, ItemData>();
        }

        public ItemData LoadItemData(string itemID)
        {
            if (_itemsData.ContainsKey(itemID))
                return _itemsData[itemID];

            ItemData itemData = Resources.Load<ItemData>(ItemsDataPath + itemID);
            _itemsData.Add(itemID, itemData);
            Debug.Log($"Loaded {itemData.ItemID} {itemData}");

            return itemData;
        }

        public ReadOnlyInventoryItemData[] LoadReadOnlyItemsDataArray(IReadOnlyInventoryItem[] items)
        {
            ReadOnlyInventoryItemData[] data = new ReadOnlyInventoryItemData[items.Length];
            ItemData currentItemData;

            for (int i = 0; i < items.Length; i++)
            {
                currentItemData = LoadItemData(items[i].ItemID);
                data[i] = new ReadOnlyInventoryItemData(
                    currentItemData.ItemID,
                    currentItemData.ItemName,
                    currentItemData.Description,
                    items[i].Amount,
                    currentItemData.StackSize,
                    currentItemData.Icon,
                    currentItemData.Actions
                );
            }

            return data;
        }
    }
}