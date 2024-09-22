using UnityEngine;

namespace RadioSilence.InventorySystem.Data
{
    [CreateAssetMenu(fileName = "ItemData", menuName = "Items/ItemData")]
    public class ItemData : ScriptableObject
    {
        [SerializeField] private string _itemName;
        [TextArea]
        [SerializeField] private string _description;
        [SerializeField] private float _mass;
        [SerializeField] private bool _isStackable;
        [SerializeField] private int _stackSize;
        [SerializeField] private ActionsWithItem _actions;
        [SerializeField] private Sprite _icon;

        public string ItemID => name;
        public string ItemName => _itemName;
        public string Description => _description;
        public float Mass => _mass;
        public bool IsStackable => _isStackable;
        public int StackSize => _stackSize;
        public ActionsWithItem Actions => _actions;
        public Sprite Icon => _icon;
    }

    public enum ActionsWithItem
    {
        None,
        Use,
        Drop,
        Unload,
    }
}