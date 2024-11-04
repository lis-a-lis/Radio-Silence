using UnityEngine;
using StalkerLikeInventory.Core;

namespace StalkerLikeInventory.Gameplay
{
    public class StalkerBackpack : MonoBehaviour
    {
        [SerializeField] private Transform _playerHead;
        [SerializeField] private LayerMask _itemLayerMask;
        [SerializeField] private Vector2Int _backpackSize;
        [SerializeField] private float _maxItemPickUpDistance;

        private IInventory _inventory;

        public IObservableInventory PlayerInventory => _inventory;

        private void Awake()
        {
            _inventory = new StalkerInventory(_backpackSize);
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
                PickUpItem();
        }

        private void PickUpItem()
        {
            if (Physics.Raycast(_playerHead.position, _playerHead.forward, out RaycastHit hit, _maxItemPickUpDistance, _itemLayerMask))
            {
                GameObject itemObject = hit.collider.gameObject;
                var item = itemObject.GetComponent<IGameplayItem>();

                if (item.PickUpItem(_inventory.AddItem(item.GetReadOnlyItemData()).addedItemsAmount))
                    Destroy(itemObject);
            }
        }
    }
}