using System;

namespace Assets.Code.Stats
{
    public struct HealthData
    {
        public readonly float maxValue;
        public float value;

        public float regenerateSpeed;
        public float maxRegeneratingPercent;

        public readonly float defaultDamageMultiplier;

        public float bloodEffectDamagePerSecond;
        public float hitEffectDamageSingle;

        public float characterVelocityReduction;
        public float maxCharacterVelocityReduction;
    }
}
