using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace RadioSilence.UI.InventoryUI
{
    public class NextInventoryPageButton : MonoBehaviour, IPointerClickHandler
    {
        public event Action<int> OnPageChanged;

        public void OnPointerClick(PointerEventData eventData)
        {
            OnPageChanged?.Invoke(1);
        }
    }
}
