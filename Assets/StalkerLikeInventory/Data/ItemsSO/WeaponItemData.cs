using UnityEngine;

namespace StalkerLikeInventory.Data
{
    [CreateAssetMenu(fileName = "WeaponItem", menuName = "StalkerLikeInventory/WeaponItem")]
    public class WeaponItemData : ItemData
    {
        [SerializeField] private float _damage;    
        [SerializeField] private float _range;

        public float Damage => _damage;
    }
}