using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code.Stats
{
    public class CharacterHealth : MonoBehaviour
    {
        [SerializeField] private BodyPartHealthData _headHealth;
        [SerializeField] private BodyPartHealthData _chestHealth;
        [SerializeField] private BodyPartHealthData _stomachHealth;

        [SerializeField] private BodyPartHealthData _leftLegHealth;
        [SerializeField] private BodyPartHealthData _rightLegHealth;

        [SerializeField] private BodyPartHealthData _leftArmHealth;
        [SerializeField] private BodyPartHealthData _rightArmHealth;

        private readonly Dictionary<BodyPartType, BodyPartHealthData> _bodyParts = new Dictionary<BodyPartType, BodyPartHealthData>();

        public void ApplyDamageToBodyPart(BodyPartType bodyPartType, DamageData damage)
        {
            var a = _bodyParts[bodyPartType];
        }
    }

    public enum BodyPartType
    {
        Head,
        Chest,
        Stomach,
        LeftLeg,
        RightLeg,
        LeftArm,
        RightArm,
    }

    public class DamagableBodyPart : MonoBehaviour
    {
        [SerializeField] private BodyPartType _bodyPartType;

        private CharacterHealth _health;

        public void InitCallbackHandler(CharacterHealth characterHealth)
        {
            _health = characterHealth;
        }

        public void ApplyDamage(DamageData damage)
        {
            _health.ApplyDamageToBodyPart(_bodyPartType, damage);
        }
    }

    [Serializable]
    public struct DamageData
    {
        public float healthDamage;
        public float bloodDamage;
    }


    [Serializable]
    public struct BodyPartHealthData
    {
        public float maxHealth;
        public float maxBlood;

        public float health;
        public float blood;

        public float maxBleedingSpeed;
        public float minBleedingSpeed;

        public float healthRecoverySpeed;
        public float bloodRecoverySpeed;

        public bool IsBleeding { get; set; }    
    }
}
