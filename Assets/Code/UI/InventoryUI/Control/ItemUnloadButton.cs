using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace RadioSilence.UI.InventoryUI.Control
{
    public class ItemUnloadButton : MonoBehaviour, IPointerClickHandler
    {
        public event Action OnClicked;

        public void OnPointerClick(PointerEventData eventData)
        {
            OnClicked?.Invoke();
        }
    }
}
