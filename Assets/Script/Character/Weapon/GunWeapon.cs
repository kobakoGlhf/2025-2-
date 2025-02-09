using MFFrameWork.Utilities;
using System.Threading;
using UnityEngine;

namespace MFFrameWork
{
    public class GunWeapon : Weapon_B
    {
        [SerializeField] Transform _muzzle;
        [SerializeField] GameObject _bulletPrefab;
        [SerializeField] float _speed=5;
        [SerializeField] float _lifeTime=10;
        protected override void Attack(Transform _target, float attackPower, CancellationToken token)
        {
            var bulletObj = Instantiate(_bulletPrefab, _muzzle.position, Quaternion.identity);

            Debug.Log(_target.position);

            if(bulletObj.TryGetComponent(out IBullet bullet))
            {
                bullet.Init(attackPower, this.gameObject.layer, _lifeTime);
            }
            var direction = (_target.position - _muzzle.position);
            bulletObj.transform.rotation = Quaternion.LookRotation(_target.position);
            if (bulletObj.TryGetComponent(out Rigidbody rb))
            {
                rb.linearVelocity = direction * _speed;
            }
        }
    }
}
