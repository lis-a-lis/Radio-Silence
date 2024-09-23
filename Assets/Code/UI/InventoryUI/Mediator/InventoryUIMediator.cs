using UnityEngine;
using RadioSilence.UI.InventoryUI.View;
using RadioSilence.InventorySystem.Data;
using RadioSilence.UI.InventoryUI.Control;
using RadioSilence.InventorySystem.GameplayComponents;

namespace RadioSilence.UI.InventoryUI.Mediator
{
    public class InventoryUIMediator : MonoBehaviour, IInventoryUIMediator
    {
        [SerializeField] private PlayerBackpack _inventory;
        [SerializeField] private InventoryInfo _info;
        [SerializeField] private InventoryGrid _grid;
        [SerializeField] private InventoryGridControl _gridControl;
        [SerializeField] private ItemActionsControl _actionsControl;
        [SerializeField] private InventoryStateView _inventoryState;

        private int _selectedItemIndex;
        private ReadOnlyItemData _selectedItemData;

        private void Awake()
        {
            _info.SetMediator(this);
            _grid.SetMediator(this);
            _inventory.SetMediator(this);
            _gridControl.SetMediator(this);
            _actionsControl.SetMediator(this);
        }

        public void NotifyPageChanged(PageChangingDirections direction)
        {
            _grid.UpdatePage(direction);
        }

        public void NotifyActionButtonClicked(ActionsWithItem actions)
        {
            if (actions == ActionsWithItem.Drop && _selectedItemIndex < _inventory.ItemsCount)
            {
                _inventory.DropItem(_selectedItemData.id, _selectedItemIndex, 1);
                UpdateInfo();
            }
        }

        public void NotifyItemViewClicked(ReadOnlyItemData data, int clickedItemIndex)
        {
            _selectedItemData = data;
            _selectedItemIndex = clickedItemIndex;
            _info.SetInfo(data);
            _actionsControl.SetActiveButtons(data.actions);
        }

        public void NotifyInventoryChanged(ReadOnlyItemData[] data)
        {
            _grid.UpdateGridData(data);
            _grid.UpdatePage();
            _inventoryState.SetMass(_inventory.Mass);
        }

        private void UpdateInfo()
        {
            int newItemAmount = _selectedItemData.amount - 1;

            if (newItemAmount == 0)
                SetNewItemInfoAtSelectedSlot();
            else
                SetInfoWithLessItemAmount(newItemAmount);
        }

        private void SetNewItemInfoAtSelectedSlot()
        {
            if (_inventory.TryGetItemAtIndex(out var data, _selectedItemIndex))
            {
                _selectedItemData = data;
                _actionsControl.SetActiveButtons(data.actions);
                _info.SetInfo(data);
            }
            else
            {
                _info.ClearInfo();
                _actionsControl.DisableButtons();
            }
        }

        private void SetInfoWithLessItemAmount(int newAmount)
        {
            ReadOnlyItemData newData = new ReadOnlyItemData(
                    _selectedItemData.id,
                    _selectedItemData.name,
                    _selectedItemData.description,
                    newAmount,
                    _selectedItemData.stackSize,
                    _selectedItemData.icon,
                    _selectedItemData.actions
                );

            _selectedItemData = newData;
            _info.SetInfo(newData);
        }
    }
}
