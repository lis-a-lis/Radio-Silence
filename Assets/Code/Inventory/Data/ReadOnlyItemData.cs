using UnityEngine;

namespace RadioSilence.InventorySystem.Data
{
    public readonly struct ReadOnlyItemData
    {
        public readonly string id;
        public readonly string name;
        public readonly string description;
        public readonly int amount;
        public readonly int stackSize;
        public readonly float mass;
        public readonly Sprite icon;
        public readonly ActionsWithItem actions;

        public ReadOnlyItemData(string id, string name, string description, float mass, Sprite icon)
        {
            this.id = id;
            this.name = name;
            this.description = description;
            this.icon = icon;
            this.mass = mass;
            amount = 1;
            stackSize = 1;
            actions = ActionsWithItem.Drop;
        }

        public ReadOnlyItemData(string id, string name, string description, int amount, int stackSize, float mass, Sprite icon, ActionsWithItem actions)
        {
            this.id = id;
            this.name = name;
            this.description = description;
            this.amount = amount;
            this.stackSize = stackSize;
            this.mass = mass;
            this.icon = icon;
            this.actions = actions;
        }

        public bool IsSingle => amount == 1;

        public bool IsNotSingle => amount > 1;
    }
}
