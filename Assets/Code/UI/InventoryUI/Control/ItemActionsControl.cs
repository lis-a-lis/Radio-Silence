using UnityEngine;
using System.Collections.Generic;
using RadioSilence.InventorySystem.Data;
using RadioSilence.UI.InventoryUI.Mediator;

namespace RadioSilence.UI.InventoryUI.Control
{
    public class ItemActionsControl : MonoBehaviour, IInventoryUIMediatorComponent
    {
        [SerializeField] private ItemUseButton _useButton;
        [SerializeField] private ItemDropButton _dropButton;
        [SerializeField] private ItemUnloadButton _unloadButton;

        private IInventoryUIMediator _mediator;

        public void SetMediator(IInventoryUIMediator mediator)
        {
            _mediator = mediator;
        }

        public void SetActiveButtons(ActionsWithItem actions)
        {
            DisableButtons();

            EnableButtons(actions);
        }

        private void Awake()
        {
            DisableButtons();
        }

        public void DisableButtons()
        {
            _useButton.gameObject.SetActive(false);
            _dropButton.gameObject.SetActive(false);
            _unloadButton.gameObject.SetActive(false);
        }

        private void EnableButtons(ActionsWithItem actions)
        {
            List<string> availableActions = EnumFlagsAttribute.GetSelectedStrings(actions);
            if (EnumFlagsAttribute.HasFlag(actions, ActionsWithItem.Use))
                _useButton.gameObject.SetActive(true);
            if (availableActions.Contains(ActionsWithItem.Drop.ToString()))
                _dropButton.gameObject.SetActive(true);
            if (availableActions.Contains(ActionsWithItem.Unload.ToString()))
                _unloadButton.gameObject.SetActive(true);
        }

        private void OnEnable()
        {
            _useButton.OnClicked += HandleUseButtonClick;
            _dropButton.OnClicked += HandleDropButtonClick;
            _unloadButton.OnClicked += HandleUnloadButtonClick;
        }

        private void OnDisable()
        {
            _useButton.OnClicked -= HandleUseButtonClick;
            _dropButton.OnClicked -= HandleDropButtonClick;
            _unloadButton.OnClicked -= HandleUnloadButtonClick;
        }

        private void HandleUseButtonClick()
        {
            _mediator.NotifyActionButtonClicked(ActionsWithItem.Use);
        }

        private void HandleDropButtonClick()
        {
            _mediator.NotifyActionButtonClicked(ActionsWithItem.Drop);
        }

        private void HandleUnloadButtonClick()
        {
            _mediator.NotifyActionButtonClicked(ActionsWithItem.Unload);
        }
    }
}
