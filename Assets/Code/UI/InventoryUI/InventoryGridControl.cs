using UnityEngine;

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
            _nextPageButton.OnPageChanged += HandlePageButtonsInput;
            _lastPageButton.OnPageChanged += HandlePageButtonsInput;
        }

        private void OnDisable()
        {
            _nextPageButton.OnPageChanged -= HandlePageButtonsInput;
            _lastPageButton.OnPageChanged -= HandlePageButtonsInput;
        }

        private void HandlePageButtonsInput(int page)
        {
            _mediator.NotifyPageChanged(page);
        }
    }
}
