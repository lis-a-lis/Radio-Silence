namespace RadioSilence.InventorySystem.Core
{
    public interface IReadOnlyInventoryItem
    {
        public string ItemID { get; }
        public int Amount { get; }
        public bool IsStackable { get; }
        public int StackSize { get; }
    }
}