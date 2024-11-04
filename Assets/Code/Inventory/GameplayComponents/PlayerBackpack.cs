using UnityEngine;
using RadioSilence.InventorySystem.Core;
using RadioSilence.InventorySystem.Data;
using RadioSilence.Services.InputServices;
using RadioSilence.UI.InventoryUI.Mediator;

namespace RadioSilence.InventorySystem.GameplayComponents
{
    public class PlayerBackpack : MonoBehaviour, IInventoryUIMediatorComponent
    {
        [SerializeField] private Transform _playerHead;
        [SerializeField] private LayerMask _itemLayerMask;
        [SerializeField] private float _maxItemPickUpDistance = 1;

        private Inventory _inventory = new Inventory();
        private IInventoryUIMediator _mediator;
        private IInputService _input;
        private CharacterStatsData _stats;

        public int ItemsCount => _inventory.GetItems().Length;

        public void SetMediator(IInventoryUIMediator mediator)
        {
            _mediator = mediator;
        }

        public void Inject(IInputService input)
        {
            _input = input;
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

        private void Awake()
        {
            _stats = new CharacterStatsData();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (Physics.Raycast(_playerHead.position, _playerHead.forward, out RaycastHit hit, _maxItemPickUpDistance, _itemLayerMask))
                {
                    GameObject itemObject = hit.collider.gameObject;
                    Item item = itemObject.GetComponent<Item>();
                    AddItemToInventory(item);
                    Destroy(itemObject);
                }
            }
        }

        private void InstantiateDroppedItem(string itemID)
        {
            Vector3 itemPosition = transform.position + Vector3.up + transform.forward / 2;
            Instantiate(ItemDataLoader.Instance.LoadItemPrefab(itemID), itemPosition, Quaternion.identity);
        }

        private void PickUpItem()
        {
            if (TryGetItemObject(out GameObject itemObject))
            {
                if (itemObject.TryGetComponent<Item>(out Item item))
                {
                    AddItemToInventory(item);
                    Destroy(itemObject);
                }
            }
        }

        private bool TryGetItemObject(out GameObject itemObject)
        {
            bool result = Physics.Raycast(_playerHead.position, _playerHead.forward, out RaycastHit hit, _maxItemPickUpDistance, _itemLayerMask);

            itemObject = result ? hit.collider.gameObject : null;

            return result;
        }

        private void AddItemToInventory(Item item)
        {
            ItemData data = item.Data;
            _inventory.AddItems(data.ItemID, 1, data.IsStackable, data.StackSize);
            _mediator.NotifyInventoryChanged(ItemDataLoader.Instance.LoadReadOnlyItemsDataArray(_inventory.GetItems()));
        }

        public void UseItem(string id, int selectedItemIndex)
        {
            var item = ItemDataLoader.Instance.LoadItemPrefab(id);

            IUsableItem usableItem = (IUsableItem)item.GetComponent<Item>();

            _inventory.RemoveItems(id, selectedItemIndex, usableItem.Use(_stats).usedAmount);
            ;
        }
    }
}