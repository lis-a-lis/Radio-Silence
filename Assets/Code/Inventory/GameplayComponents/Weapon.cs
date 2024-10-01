using UnityEngine;

namespace RadioSilence.InventorySystem.GameplayComponents
{
    public class Weapon : Item, IWearableItem
    {

        public void Wear(Transform wearPoint)
        {
            transform.parent = wearPoint;
            transform.position = Vector3.zero;
        }
    }
}