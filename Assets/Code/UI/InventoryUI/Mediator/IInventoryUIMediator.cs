using RadioSilence.InventorySystem.Data;

namespace RadioSilence.UI.InventoryUI.Mediator
{
    public enum PageChangingDirections
    {
        Previous = -1,
        Current = 0,
        Next = 1,
    }

    public interface IInventoryUIMediator
    {
        public void NotifyPageChanged(PageChangingDirections direction);
        public void NotifyActionButtonClicked(ActionsWithItem action);
        public void NotifyInventoryChanged(ReadOnlyItemData[] data);
        public void NotifyItemViewClicked(ReadOnlyItemData data, int clickedItemIndex);
    }
}
