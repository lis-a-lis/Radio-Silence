using UnityEngine;
using RadioSilence.UI.InventoryUI.Control;
using RadioSilence.UI.InventoryUI.Mediator;

namespace RadioSilence.UI.InventoryUI
{
    public class InventoryGridControl : MonoBehaviour, IInventoryUIMediatorComponent
    {
        [SerializeField] private NextInventoryPageButton _nextPageButton;
        [SerializeField] private LastInventoryPageButton _lastPageButton;

        private int _currentPage;
        private IInventoryUIMediator _mediator;

        public void SetMediator(IInventoryUIMediator mediator)
        {
            _mediator = mediator;
        }

        private void OnEnable()
        {
            _nextPageButton.OnPageChanged += HandleNextPageButtonInput;
            _lastPageButton.OnPageChanged += HandleLastPageButtonInput;
        }

        private void OnDisable()
        {
            _nextPageButton.OnPageChanged -= HandleNextPageButtonInput;
            _lastPageButton.OnPageChanged -= HandleLastPageButtonInput;
        }

        private void HandleNextPageButtonInput()
        {
            _mediator.NotifyPageChanged(PageChangingDirections.Next);
        }

        private void HandleLastPageButtonInput()
        {
            _mediator.NotifyPageChanged(PageChangingDirections.Previous);
        }
    }
}
