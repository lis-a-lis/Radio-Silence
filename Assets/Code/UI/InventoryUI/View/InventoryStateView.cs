using UnityEngine;
using TMPro;
using System;

namespace RadioSilence.UI.InventoryUI.View
{
    public class InventoryStateView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _mass;

        public void SetMass(float mass)
        {
            _mass.text = Math.Round((double)mass, 2).ToString() + " kg";
        }
    }
}
