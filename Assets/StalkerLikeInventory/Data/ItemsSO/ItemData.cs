using System;
using UnityEngine;
using System.Collections.Generic;
using StalkerLikeInventory.Core;

namespace StalkerLikeInventory.Data
{
    [CreateAssetMenu(fileName = "Item", menuName = "StalkerLikeInventory/Item")]
    public class ItemData : ScriptableObject
    {
        [SerializeField] private Sprite _icon;
        [SerializeField] private string _name;
        [TextArea][SerializeField] private string _description;
        [SerializeField] private float _mass;
        [SerializeField] private float _strength;
        [SerializeField] private bool _isStackable;
        [Min(1)][SerializeField] private int _stackSize = 1;
        [SerializeField] private Vector2Int _size = Vector2Int.one;

        public string ItemID => name;
        public Sprite Icon => _icon;
        public string Name => _name;
        public string Description => _description;
        public float Mass => _mass;
        public float Strength => _strength;
        public Vector2Int Size => _size;
        public bool IsStackable => _isStackable;
        public int StackSize => _stackSize;
    }

    public class BackpackData : ItemData
    {
        [SerializeField] private Vector2Int _capacity;

        public Vector2Int Capacity => _capacity;
    }

    public class GasMaskData : ItemData
    {
        [SerializeField] private float _radiationProtection = 1;

        public float RadiationProtection => _radiationProtection;
    }

    public class GasMaskFilterData : ItemData
    {
        [SerializeField] private float _medianUseDurationInSeconds;

        public float MedianUseDuration => _medianUseDurationInSeconds;
    }

    [CreateAssetMenu(fileName = "Crafting Recipe", menuName = "StalkerLikeInventory/Crafting Recipe")]
    public class CraftingRecipe : ScriptableObject
    {
        [SerializeField] private ItemData _craftableItem;
        [Min(1), SerializeField] private int _craftableAmount = 1;
        [SerializeField] private List<RequiredForCraftingItemData> _requiredItems;

        public CraftingItemResult TryCraft(IInventory inventory)
        {
            if (!IsCanCreate(inventory))
                return new CraftingItemResult(false, 0, 0);

            foreach (var requiredItem in _requiredItems)
                inventory.RemoveItem(requiredItem.itemData.ItemID, requiredItem.amount);

            ReadOnlyItemData craftedItemData = new ReadOnlyItemData(_craftableItem, _craftableAmount);
            AddItemsToInventoryResult addResult = inventory.AddItem(craftedItemData);

            if (addResult.AllItemsAdded)
                return new CraftingItemResult(true, _craftableAmount, addResult.addedItemsAmount);

            ReadOnlyItemData notAddedItemData =
                new ReadOnlyItemData(craftedItemData, addResult.itemsToAddAmount - addResult.addedItemsAmount);

            return new CraftingItemResult(true, _craftableAmount, addResult.addedItemsAmount, notAddedItemData);
        }

        private bool IsCanCreate(IInventory inventory)
        {
            foreach (var requiredItem in _requiredItems)
            {
                if (!inventory.HasItem(requiredItem.itemData.ItemID, requiredItem.amount))
                    return false;
            }
            return true;
        }
    }

    [Serializable]
    public class RequiredForCraftingItemData
    {
        public ItemData itemData;
        public int amount;
    }

    public readonly struct CraftingItemResult
    {
        public readonly bool success;
        public readonly int addedItemAmount;
        public readonly int craftedItemAmount;
        public readonly ReadOnlyItemData notAddedItemData;

        public CraftingItemResult(bool success, int craftedItemAmount, int addedItemAmount, ReadOnlyItemData notAddedItemData = default)
        {
            this.success = success;
            this.craftedItemAmount = craftedItemAmount;
            this.addedItemAmount = addedItemAmount;
            this.notAddedItemData = notAddedItemData;
        }

        public bool AllCraftedItemsAdded => addedItemAmount == craftedItemAmount;

        public bool AnyCraftedItemDropped => addedItemAmount < craftedItemAmount;
    }
}