﻿using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace RadioSilence.UI.InventoryUI.Control
{
    public class ItemUseButton : MonoBehaviour, IPointerClickHandler
    {
        public event Action OnClicked;

        public void OnPointerClick(PointerEventData eventData)
        {
            OnClicked?.Invoke();
        }
    }
}