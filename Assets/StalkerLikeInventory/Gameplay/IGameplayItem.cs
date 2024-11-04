using StalkerLikeInventory.Data;

namespace StalkerLikeInventory.Gameplay
{
    public interface IGameplayItem
    {
        public ReadOnlyItemData GetReadOnlyItemData();

        public bool PickUpItem(int amount);
    }
}