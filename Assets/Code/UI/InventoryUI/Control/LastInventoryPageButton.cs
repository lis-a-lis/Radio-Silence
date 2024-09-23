using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace RadioSilence.UI.InventoryUI.Control
{
    public class LastInventoryPageButton : MonoBehaviour, IPointerClickHandler
    {
        public event Action OnPageChanged;

        public void OnPointerClick(PointerEventData eventData)
        {
            OnPageChanged?.Invoke();
        }
    }
}
