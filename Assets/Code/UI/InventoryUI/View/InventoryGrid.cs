using UnityEngine;
using System.Collections.Generic;
using RadioSilence.InventorySystem.Data;
using RadioSilence.UI.InventoryUI.Mediator;

namespace RadioSilence.UI.InventoryUI.View
{
    public class InventoryGrid : MonoBehaviour, IInventoryUIMediatorComponent
    {
        public const int PAGE_CAPACITY = 20;

        private int _currentPageIndex = 0;
        private IInventoryUIMediator _mediator;
        private List<InventorySlotView> _views;
        private ReadOnlyItemData[] _itemsData;

        private void Awake()
        {
            _views = new List<InventorySlotView>();
            _views.AddRange(gameObject.GetComponentsInChildren<InventorySlotView>());
        }

        public void SetMediator(IInventoryUIMediator mediator)
        {
            _mediator = mediator;
        }

        public void UpdateGridData(ReadOnlyItemData[] data)
        {
            _itemsData = data;
            if (data.Length / PAGE_CAPACITY < _currentPageIndex)
                _currentPageIndex -= 1;
        }

        public void UpdatePage(PageChangingDirections direction = PageChangingDirections.Current)
        {
            int maxPageIndex = _itemsData.Length / PAGE_CAPACITY;
            int newPageIndex = _currentPageIndex;

            if (direction == PageChangingDirections.Next)
                newPageIndex += 1;
            else if (direction == PageChangingDirections.Previous)
                newPageIndex -= 1;

            if (newPageIndex < 0 || newPageIndex > maxPageIndex)
                return;

            _currentPageIndex = newPageIndex;
            int pageStartIndex = _currentPageIndex * PAGE_CAPACITY;
            int pageEndIndex = Mathf.Clamp(pageStartIndex + PAGE_CAPACITY, pageStartIndex, _itemsData.Length);
            int pageLength = pageEndIndex - pageStartIndex; 
            int index = 0;

            ReadOnlyItemData[] data = new ReadOnlyItemData[pageLength];
       
            for (int i = 0; i < data.Length; i++)
                data[i] = _itemsData[pageStartIndex + i];

            foreach (InventorySlotView view in _views)
            {
                view.Clear();

                if (index < data.Length)
                    view.SetView(data[index]);

                index++;
            }
        }

        private void OnEnable()
        {
            foreach (InventorySlotView view in _views)
                view.OnClicked += HandleSlotViewClick;
        }

        private void OnDisable()
        {
            foreach (InventorySlotView view in _views)
                view.OnClicked -= HandleSlotViewClick;
        }

        private void HandleSlotViewClick(InventorySlotView view, ReadOnlyItemData data)
        {
            int clickedItemIndex = _currentPageIndex * PAGE_CAPACITY + _views.IndexOf(view);
            _mediator.NotifyItemViewClicked(data, clickedItemIndex);
        }
    }
}
