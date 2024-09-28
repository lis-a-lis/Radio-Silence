using UnityEngine;
using TMPro;
using System;
using RadioSilence.InventorySystem.Data;

namespace RadioSilence.UI.InventoryUI.View
{
    public class InventoryItemsMassView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _mass;

        public void SetData(ReadOnlyItemData[] inventoryItems)
        {
            _mass.text = Math.Round((double)CalculateMass(inventoryItems), 2).ToString() + " kg";
        }

        private float CalculateMass(ReadOnlyItemData[] data)
        {
            float mass = 0;

            for (int i = 0; i < data.Length; i++) {
                mass += data[i].mass * data[i].amount;
            }

            return mass;
        }
    }
}
