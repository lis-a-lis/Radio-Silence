using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace RadioSilence.UI.InventoryUI.Control
{
    public class NextInventoryPageButton : MonoBehaviour, IPointerClickHandler
    {
        public event Action OnPageChanged;

        public void OnPointerClick(PointerEventData eventData)
        {
            OnPageChanged?.Invoke();
        }
    }
}
