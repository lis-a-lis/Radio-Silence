namespace StalkerLikeInventory.Core
{
    public readonly struct AddItemsToInventoryResult
    {
        public readonly int addedItemsAmount;
        public readonly int itemsToAddAmount;

        public AddItemsToInventoryResult(int itemsToAddAmount, int addedItemsAmount)
        {
            this.itemsToAddAmount = itemsToAddAmount;
            this.addedItemsAmount = addedItemsAmount;
        }

        public bool AllItemsAdded => addedItemsAmount == itemsToAddAmount;
    }
}