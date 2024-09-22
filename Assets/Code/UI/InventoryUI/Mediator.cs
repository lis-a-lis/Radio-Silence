using UnityEngine;
using RadioSilence.InventorySystem.Data;
using RadioSilence.InventorySystem.GameplayComponents;

namespace RadioSilence.UI.InventoryUI
{
    public class Mediator : MonoBehaviour, IInventoryUIMediator
    {
        [SerializeField] private PlayerBackpack _inventory;
        [SerializeField] private InventoryInfo _info;
        [SerializeField] private InventoryGrid _grid;
        [SerializeField] private InventoryGridControl _gridControl;
        [SerializeField] private ItemActionsControl _actionsControl;

        private ReadOnlyInventoryItemData _selectedItemData;
        private int _selectedItemIndex;

        private void Awake()
        {
            _info.SetMediator(this);
            _grid.SetMediator(this);
            _inventory.SetMediator(this);
            _gridControl.SetMediator(this);
            _actionsControl.SetMediator(this);
        }

        public void NotifyInfoChanged(ActionsWithItem actions)
        {
        }

        public void NotifyPageChanged(int pageIndex)
        {
            _grid.UpdatePage(pageIndex);
            Debug.Log(pageIndex);
        }

        public void NotifyActionButtonClicked(ActionsWithItem actions)
        {
            if (actions == ActionsWithItem.Drop && _selectedItemIndex < _inventory.ItemsCount)
            {
                _inventory.DropItem(_selectedItemData.id, _selectedItemIndex, 1);
                UpdateInfoAfterDropItems();
            }
        }

        public void NotifyItemViewClicked(ReadOnlyInventoryItemData data, int clickedItemIndex)
        {
            _selectedItemData = data;
            _selectedItemIndex = clickedItemIndex;
            _info.SetInfo(data);
            _actionsControl.SetActiveButtons(data.actions);
        }

        public void NotifyInventoryChanged(ReadOnlyInventoryItemData[] data)
        {
            _grid.UpdateGridData(data);
            _grid.UpdatePage(0);
            Debug.Log(data.Length);
        }

        private void UpdateInfoAfterDropItems()
        {
            int newItemAmount = _selectedItemData.amount - 1;

            if (newItemAmount == 0)
            {
                _info.ClearInfo();
                _actionsControl.DisableButtons();
            }
            else
            {
                ReadOnlyInventoryItemData newData = new ReadOnlyInventoryItemData(
                    _selectedItemData.id,
                    _selectedItemData.name,
                    _selectedItemData.description,
                    newItemAmount,
                    _selectedItemData.stackSize,
                    _selectedItemData.icon,
                    _selectedItemData.actions
                );

                _selectedItemData = newData;
                _info.SetInfo(newData);
            }
            
        }
    }
}
