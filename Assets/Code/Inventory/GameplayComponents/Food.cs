using UnityEngine;

namespace RadioSilence.InventorySystem.GameplayComponents
{
    public class Food : Item, IUsableItem
    {
        [SerializeField] private float _calorificValue = 10;
        [SerializeField] private float _healthRecovery = 1;

        public ItemUseStatus Use(CharacterStatsData stats)
        {
            stats.Hunger += _calorificValue;
            stats.Health += _healthRecovery;

            return new ItemUseStatus(1);
        }
    }
}