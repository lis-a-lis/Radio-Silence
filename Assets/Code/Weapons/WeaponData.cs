using UnityEngine;

namespace RadioSilence.Weapons
{
    [CreateAssetMenu(fileName = "WeaponData", menuName = "Weapons/WeaponData")]
    public class WeaponData : ScriptableObject
    {
        public float baseDamage;
        [Range(0, 100)] public float flatness;
        [Range(0, 100)] public float accuracy;
        [Range(0, 100)] public int ergonomics;
        public int range;
        public int rate;
        public int startBulletSpeed;
        public int strength;
        [Range(0, 100)] public float heatPerShot;
    }
}