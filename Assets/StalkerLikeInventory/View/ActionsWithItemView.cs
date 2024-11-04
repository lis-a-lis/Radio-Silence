using System.Collections.Generic;
using NUnit.Framework;
using RadioSilence.InventorySystem.Data;
using StalkerLikeInventory.Core;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace StalkerLikeInventory.View
{
    public class ActionsWithItemView : MonoBehaviour
    { }/*
        [SerializeField] private ActionButton _actionButtonPrefab;

        private List<ActionButton> _actionButtons = new List<ActionButton>();
        private Image _background;

        public void Enable(Vector2 screenPosition, Vector2Int positionInInventory, GameplayItem item, PlayerStats stats, StalkerInventory inventory)
        {
            var button = InitNewButton();
            _actionButtons.Add(button);
            button.Button.onClick.AddListener(() => inventory.RemoveItem(positionInInventory, out var item));
            
            if (item is IUsable)
            {
                button = InitNewButton();
                _actionButtons.Add(button);
                button.Button.onClick.AddListener(() => ((IUsable)item).Use(stats));
            }

            transform.position = screenPosition;
            _background.rectTransform.sizeDelta = new Vector2(200, _actionButtons.Count * 30);
        }

        public void Disable()
        {
            _actionButtons.Clear();
        }

        private ActionButton InitNewButton()
        {
            return Instantiate(_actionButtonPrefab, transform);
        }
    }*/
}