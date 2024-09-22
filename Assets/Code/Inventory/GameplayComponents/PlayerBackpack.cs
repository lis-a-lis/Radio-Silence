using UnityEngine;
using RadioSilence.InventorySystem.Core;
using RadioSilence.InventorySystem.Data;
using RadioSilence.UI.InventoryUI;

namespace RadioSilence.InventorySystem.GameplayComponents
{
    public class PlayerBackpack : MonoBehaviour, IInventoryUIMediatorComponent
    {
        [SerializeField] private Transform _playerHead;
        [SerializeField] private float _maxItemPickUpDistance = 1;

        private Inventory _inventory = new Inventory();
        private IInventoryUIMediator _mediator;

        public int ItemsCount => _inventory.GetItems().Length;

        public void SetMediator(IInventoryUIMediator mediator)
        {
            _mediator = mediator;
        }

        public void DropItem(string itemID, int itemIndex, int amount)
        {
            _inventory.RemoveItems(itemID, itemIndex, amount);
            _mediator.NotifyInventoryChanged(ItemsDataLoader.Instance.LoadReadOnlyItemsDataArray(_inventory.GetItems()));
        }

        private void Start()
        {
            ItemData data = ItemsDataLoader.Instance.LoadItemData("MeetCan");
            _inventory.AddItems(data.ItemID, 30, data.IsStackable, data.StackSize, data.Mass);
            _mediator.NotifyInventoryChanged(ItemsDataLoader.Instance.LoadReadOnlyItemsDataArray(_inventory.GetItems()));
        }

        private void Update()
        {
            PickUpItem();
        }

        private void PickUpItem()
        {
            RaycastHit hit;

            if (Physics.Raycast(_playerHead.position, _playerHead.forward, out hit, _maxItemPickUpDistance))
            {
                if (hit.collider.gameObject.TryGetComponent<Item>(out Item item) && Input.GetMouseButtonDown(0))
                {
                    ItemData data = item.Data;
                    Debug.Log(data == null);
                    Debug.Log(_inventory == null);
                    Destroy(hit.collider.gameObject);

                    _inventory.AddItems(data.ItemID, 1, data.IsStackable, data.StackSize, data.Mass);
                    _mediator.NotifyInventoryChanged(ItemsDataLoader.Instance.LoadReadOnlyItemsDataArray(_inventory.GetItems()));
                }
            }
        }
    }
}