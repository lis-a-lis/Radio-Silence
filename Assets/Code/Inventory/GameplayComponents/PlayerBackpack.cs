using UnityEngine;
using RadioSilence.InventorySystem.Core;
using RadioSilence.InventorySystem.Data;
using RadioSilence.UI.InventoryUI.Mediator;

namespace RadioSilence.InventorySystem.GameplayComponents
{
    public class PlayerBackpack : MonoBehaviour, IInventoryUIMediatorComponent
    {
        [SerializeField] private Transform _playerHead;
        [SerializeField] private float _maxItemPickUpDistance = 1;

        private IInventoryUIMediator _mediator;
        private Inventory _inventory = new Inventory();

        public int ItemsCount => _inventory.GetItems().Length;

        public void SetMediator(IInventoryUIMediator mediator)
        {
            _mediator = mediator;
        }

        public bool TryGetItemAtIndex(out ReadOnlyItemData item, int index)
        {
            item = new ReadOnlyItemData();

            if (index < ItemsCount)
            {
                item = ItemDataLoader.Instance.LoadReadOnlyItemData(_inventory.GetItems()[index]);
                return true;
            }

            return false;
        }

        public void DropItem(string itemID, int itemIndex, int amount)
        {
            _inventory.RemoveItems(itemID, itemIndex, amount);
            InstantiateDroppedItem(itemID);
            _mediator.NotifyInventoryChanged(ItemDataLoader.Instance.LoadReadOnlyItemsDataArray(_inventory.GetItems()));
        }

        private void Update()
        {
            PickUpItem();
        }

        private void InstantiateDroppedItem(string itemID)
        {
            Vector3 itemPosition = transform.position + Vector3.up + transform.forward / 2;
            Instantiate(ItemDataLoader.Instance.LoadItemPrefab(itemID), itemPosition, Quaternion.identity);
        }

        private void PickUpItem()
        {
            if (Physics.Raycast(_playerHead.position, _playerHead.forward, out RaycastHit hit, _maxItemPickUpDistance))
            {
                if (hit.collider.gameObject.TryGetComponent<Item>(out Item item) && Input.GetMouseButtonDown(0))
                {
                    ItemData data = item.Data;
                    Destroy(hit.collider.gameObject);
                    _inventory.AddItems(data.ItemID, 1, data.IsStackable, data.StackSize);
                    _mediator.NotifyInventoryChanged(ItemDataLoader.Instance.LoadReadOnlyItemsDataArray(_inventory.GetItems()));
                }
            }
        }
    }
}