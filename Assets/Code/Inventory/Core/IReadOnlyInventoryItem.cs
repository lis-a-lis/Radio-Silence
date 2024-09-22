namespace RadioSilence.InventorySystem.Core
{
    public interface IReadOnlyInventoryItem
    {
        public string ItemID { get; }
        public int Amount { get; }
        public float Mass { get; }
        public bool IsEmpty { get; }
        public bool IsStackable { get; }
        public int StackSize { get; }
    }
}