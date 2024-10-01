using UnityEngine;
using RadioSilence.InventorySystem.Data;

namespace RadioSilence.InventorySystem.GameplayComponents
{
    public class Item : MonoBehaviour
    {
        [SerializeField] private ItemData _data;

        public ItemData Data => _data;
    }

    public class CharacterStatsData
    {
        public float Hunger { get; set; } = 100;
        public float Health { get; set; } = 100;
        public float Nicotine { get; set; } = 0;
    }

    public interface IUsableItem
    {
        public ItemUseStatus Use(CharacterStatsData stats);
    }

    public interface IWearableItem
    {
        public void Wear(Transform wearPoint);
    }

    public readonly struct ItemUseStatus
    {
        public readonly int usedAmount;

        public ItemUseStatus(int usedAmount)
        {
            this.usedAmount = usedAmount;
        }
    }
}