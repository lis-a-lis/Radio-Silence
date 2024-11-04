namespace StalkerLikeInventory.Core
{
    public readonly struct RemoveItemsFromInventoryResult
    {
        public readonly bool success;
        public readonly int removedItemsAmount;
        public readonly int itemsToRemoveAmount;

        public RemoveItemsFromInventoryResult(bool success, int itemsToRemoveAmount, int removedItemsAmount)
        {
            this.success = success;
            this.removedItemsAmount = removedItemsAmount;
            this.itemsToRemoveAmount = itemsToRemoveAmount;
        }
    }
}