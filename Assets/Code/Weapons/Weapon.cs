using System.Collections.Generic;
using UnityEngine;

namespace RadioSilence.Weapons
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private WeaponData _data;
        [SerializeField] private AnimationCurve _derivationByRange;

        private List<BulletData> _bullets = new List<BulletData>();
        private float _heat;
        private float _strength;

        public bool Shot()
        {/*
            if (!magazine.GetAmmo())
                return false;
        */
            float rayCastDistance = _data.startBulletSpeed * Time.fixedDeltaTime;

            Vector3 velocity = CalculateBulletVelocity();

            Vector3 position = transform.position + transform.forward;

            _bullets.Add(new BulletData(position, velocity, 30f));

            if (Physics.Raycast(position, velocity, out RaycastHit hit))
            {
                Debug.Log("fixed point " + hit.point);
                Debug.DrawLine(hit.point, hit.point - Vector3.right, Color.green, 60);
                Debug.DrawLine(hit.point, hit.point - Vector3.up, Color.blue, 60);
                Debug.DrawLine(hit.point, hit.point - Vector3.forward, Color.white, 60);
            }

            return true;
        }

        private Vector3 CalculateBulletVelocity()
        {
            return transform.forward * _data.startBulletSpeed + Physics.gravity;// + HorizontalBulletDeflection;
        }

        private Vector3 RecalculateBulletVelocityPerFrame(Vector3 currentVelocity)
        {
            return currentVelocity + Physics.gravity * Time.fixedDeltaTime;
        }

        private Vector3 HorizontalBulletDeflection =>
            (1 - (_data.accuracy / 100f)) * Random.insideUnitCircle;




        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Shot();
            }
        }

        private void FixedUpdate()
        {
            if (_bullets.Count == 0)
                return;

            int deletedBulletsCount = 0;
            int lastBulletIndex = _bullets.Count;

            for (int i = 0; i < lastBulletIndex; i++)
            {
                if (_bullets[i].lifeTime <= 0)
                    continue;

                float rayCastDistance = _bullets[i].velocity.magnitude * Time.fixedDeltaTime;
                Vector3 position = _bullets[i].position + _bullets[i].velocity * Time.fixedDeltaTime;

                Debug.DrawLine(_bullets[i].position, position, Color.red, 2f);

                if (Physics.Raycast(_bullets[i].position, _bullets[i].velocity, out RaycastHit hit, rayCastDistance))
                {
                    ApplyShotDamage(hit.collider.gameObject);
                    deletedBulletsCount++;
                    _bullets.RemoveAt(i);
                    lastBulletIndex -= deletedBulletsCount;
                    i--;
                    Debug.Log("real point " + hit.point);
                    Debug.DrawLine(hit.point, hit.point + Vector3.right, Color.green, 60);
                    Debug.DrawLine(hit.point, hit.point + Vector3.up, Color.blue, 60);
                    Debug.DrawLine(hit.point, hit.point + Vector3.forward, Color.red, 60);
                    continue;
                }

               
               /* Debug.Log($">> Ray distance= {rayCastDistance}\n" +
                          $"\t Position= {position}" +
                          $"\t Velocity= { _bullets[i].velocity}");
*/
                _bullets[i] = new BulletData(position, RecalculateBulletVelocityPerFrame(_bullets[i].velocity), _bullets[i].lifeTime - Time.fixedDeltaTime);
            }
        }

        private void ApplyShotDamage(GameObject hittedObject)
        {
            Debug.Log($"DAMAGED {hittedObject.name}");
        }
    }

    public struct BulletData
    {
        public Vector3 position;
        public Vector3 velocity;
        public float lifeTime;

        public BulletData(Vector3 position, Vector3 velocity, float lifeTime)
        {
            this.position = position;
            this.velocity = velocity;
            this.lifeTime = lifeTime;
        }
    }

    public class WeaponMagazine
    {
        private AmmoType[] _ammos;

        public int Capacity { get { return _ammos.Length; } }
        public int Ammo { get; private set; }
        public AmmoType NextAmmoType { get; private set; }

        public bool GetAmmo()
        {
            if (Ammo == 0)
                return false;

            Ammo -= 1;
            return true;
        }
    }

    public enum AmmoType
    {
        Ammo_9x39_Broken,
        Ammo_9x39,
        Ammo_9x39_Piercing,

        Ammo_5_45x39_Broken,
        Ammo_5_45x39,
        Ammo_5_45x39_Piercing,

        Ammo_12_70_Shot_Broken,
        Ammo_12_70_Shot,
        Ammo_12_70_Bullet_Broken,
        Ammo_12_70_Bullet,
    }
}