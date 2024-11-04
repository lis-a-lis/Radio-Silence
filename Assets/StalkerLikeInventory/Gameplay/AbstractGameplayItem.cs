using StalkerLikeInventory.Data;
using UnityEngine;
using System;

namespace StalkerLikeInventory.Gameplay
{
    public abstract class AbstractGameplayItem<SourceData> : MonoBehaviour, IGameplayItem where SourceData : ItemData
    {
        [SerializeField] private SourceData _data;
        [SerializeField] private int _amount = 1;

        protected SourceData Data => _data;

        public ReadOnlyItemData GetReadOnlyItemData() =>
            new ReadOnlyItemData(_data, _amount);

        public bool PickUpItem(int amount)
        {
            if (_amount - amount < 0)
                throw new ArgumentException();

            _amount -= amount;

            return _amount == 0;
        }
    }

    public class PlayerStats : MonoBehaviour
    {
        public Health Health { get; private set; }
    }

    public class Health
    {
        private float _maxHealth;
        private float _health;

        public Health(float maxHealth)
        {
            _maxHealth = maxHealth;
            _health = maxHealth;
        }

        public void ApplyDamage(float damage)
        {
            _health = Mathf.Clamp(_health - damage, 0, _maxHealth);
        }

        public void RestoreHealth(float amount)
        {
            _health = Mathf.Clamp(_health + amount, _health, _maxHealth);
        }
    }

    public interface IUsable
    {
        public void Use(PlayerStats stats);
    }

    public interface IEquipable
    {
        public void Equip(IEquipment applyingOwner);
        public void TakeOff(IEquipment applyingOwner);
    }

    public interface IEquipableItemData
    {

    }

   /* public class GasMaskGP5 : AbstractGameplayItem<GasMaskData>, IEquipable
    {
        public void Equip(IEquipment applyingOwner)
        {

        }

        public void TakeOff(IEquipment applyingOwner)
        {
        }
    }
*/
    public enum EquipableBodyPart
    {
        Head,
        Body,
    }

    public interface IEquipment
    {
        public void EquipItem(IEquipable item);
        public void TakeOffItem(IEquipable item);

    }

    public class PlayerEquipment : MonoBehaviour, IEquipment
    {
        private float _bodyArmor;
        private float _headArmor;
        private float _radiationProtection;
        private float _slowdown;

        public void EquipGasMask(float radiationProtection, float headArmor)
        {

        }

        public void EquipItem(IEquipable equipable)
        {
            equipable.Equip(this);
        }

        public void TakeOffItem(IEquipable item)
        {

        }
    }
}