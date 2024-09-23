using TMPro;
using UnityEngine;
using UnityEngine.UI;
using RadioSilence.InventorySystem.Data;
using RadioSilence.UI.InventoryUI.Mediator;

namespace RadioSilence.UI.InventoryUI.View
{
    public class InventoryInfo : MonoBehaviour, IInventoryUIMediatorComponent
    {
        [SerializeField] private Image _itemIcon;
        [SerializeField] private TextMeshProUGUI _name;
        [SerializeField] private TextMeshProUGUI _amount;
        [SerializeField] private TextMeshProUGUI _description;
        [SerializeField] private TextMeshProUGUI _itemStateDescription;

        private IInventoryUIMediator _mediator;
        private readonly Color _defaultColor = new Color(1, 1, 1, 0);

        public void SetInfo(ReadOnlyItemData data)
        {
            _name.text = data.name;
            _amount.text = data.amount.ToString();
            _description.text = data.description;
            _itemIcon.color = Color.white;
            _itemIcon.sprite = data.icon;
        }

        public void ClearInfo()
        {
            _name.text = string.Empty;
            _amount.text = string.Empty;
            _description.text = string.Empty;
            _itemIcon.sprite = null;
            _itemIcon.color = _defaultColor;
        }

        public void SetMediator(IInventoryUIMediator mediator)
        {
            _mediator = mediator;
        }
    }
}
