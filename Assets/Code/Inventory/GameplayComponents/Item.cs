using UnityEngine;
using RadioSilence.InventorySystem.Data;

namespace RadioSilence.InventorySystem.GameplayComponents
{
    public class Item : MonoBehaviour
    {
        [SerializeField] private ItemData _data;

        public ItemData Data => _data;
    }
}