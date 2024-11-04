using System;
using UnityEngine;
using StalkerLikeInventory.Data;

namespace StalkerLikeInventory.Core
{
    public class InventorySlot
    {
        private string _itemID;
        private Vector2Int _size;
        private bool _isStackable;
        private int _stackSize;
        private float _strength;
        private float _mass;
        private int _amount;

        public InventorySlot(ReadOnlyItemData data)
        {
            _itemID = data.itemID;
            _size = data.size;
            _isStackable = data.isStackable;
            _stackSize = data.stackSize;
            _mass = data.mass;
            _strength = data.strength;
            _amount = data.amount;
        }

        public ReadOnlyItemData ReadOnlyData =>
            new ReadOnlyItemData(_itemID, _amount, _mass, _strength, _size, _isStackable, _stackSize);

        public string ItemID => _itemID;

        public float Strength
        {
            get => _strength;
            set
            {
                _strength = value;
            }
        }

        public float Mass => Amount * _mass;

        public int Amount
        {
            get => _amount;
            set
            {
                if (value < 0)
                    throw new InvalidOperationException();

                _amount = value;
            }
        }

        public Vector2Int Size => _size;

        public bool IsStackable => _isStackable;

        public int StackSize => _stackSize;

        public bool IsEmpty => Amount == 0;

        public int AvailablePlace => StackSize - Amount;

        public bool IsAvailableToAdd => Amount < StackSize;
    }
}