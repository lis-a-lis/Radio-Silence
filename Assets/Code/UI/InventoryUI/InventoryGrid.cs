using UnityEngine;
using System.Collections.Generic;
using RadioSilence.InventorySystem.Data;
using System;

namespace RadioSilence.UI.InventoryUI
{
    public class InventoryGrid : MonoBehaviour, IInventoryUIMediatorComponent
    {
        public const int PAGE_CAPACITY = 20;

        private IInventoryUIMediator _mediator;
        private List<InventorySlotView> _views;
        private ReadOnlyInventoryItemData[] _itemsData;
        private int _currentPageIndex = 0;

        private void Awake()
        {
            _views = new List<InventorySlotView>();
            _views.AddRange(gameObject.GetComponentsInChildren<InventorySlotView>());
        }

        public void SetMediator(IInventoryUIMediator mediator)
        {
            _mediator = mediator;
        }

        public void UpdateGridData(ReadOnlyInventoryItemData[] data)
        {
            _itemsData = data;
            if (data.Length / _views.Count < _currentPageIndex)
                _currentPageIndex -= 1;
        }

        public void UpdatePage(int nextPageIndex)
        {
            int maxPageIndex = _itemsData.Length / _views.Count;
            int newPageIndex = _currentPageIndex + nextPageIndex;

            if (newPageIndex < 0 || newPageIndex > maxPageIndex)
                return;

            _currentPageIndex = newPageIndex;
            int pageStartIndex = _currentPageIndex * PAGE_CAPACITY;
            int pageEndIndex = Mathf.Clamp(pageStartIndex + PAGE_CAPACITY, pageStartIndex, _itemsData.Length);
            int pageLength = pageEndIndex - pageStartIndex; 
            int index = 0;

            ReadOnlyInventoryItemData[] data = new ReadOnlyInventoryItemData[pageLength];
            Debug.Log($"data length={data.Length} next page i ={nextPageIndex}" +
                $"pageStart={pageStartIndex}  pageEnd={pageEndIndex}");
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

        private void HandleSlotViewClick(InventorySlotView view, ReadOnlyInventoryItemData data)
        {
            int clickedItemIndex = _currentPageIndex * PAGE_CAPACITY + _views.IndexOf(view);
            _mediator.NotifyItemViewClicked(data, clickedItemIndex);
        }
    }
}
