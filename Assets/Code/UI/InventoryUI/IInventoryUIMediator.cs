using RadioSilence.InventorySystem.Data;
using Unity.VisualScripting.Antlr3.Runtime;

namespace RadioSilence.UI.InventoryUI
{
    public interface IInventoryUIMediator
    {
        public void NotifyPageChanged(int pageIndex);
        public void NotifyInfoChanged(ActionsWithItem actions);
        public void NotifyActionButtonClicked(ActionsWithItem action);
        public void NotifyInventoryChanged(ReadOnlyInventoryItemData[] data);
        public void NotifyItemViewClicked(ReadOnlyInventoryItemData data, int clickedItemIndex);
    }
}
