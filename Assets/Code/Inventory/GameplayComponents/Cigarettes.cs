using UnityEngine;

namespace RadioSilence.InventorySystem.GameplayComponents
{
    public class Cigarettes : Item, IUsableItem
    {
        [SerializeField, Range(0.1f, 2f)] private float _nicotineAmount = 0.5f;

        public ItemUseStatus Use(CharacterStatsData stats)
        {
            stats.Nicotine += _nicotineAmount;

            return new ItemUseStatus(1);
        }
    }
}