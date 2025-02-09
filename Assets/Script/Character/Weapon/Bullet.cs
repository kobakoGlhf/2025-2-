
using MFFrameWork.Utilities;
using UnityEngine;

namespace MFFrameWork
{
    [RequireComponent(typeof(CapsuleCollider), typeof(Rigidbody))]
    public class Bullet : Bullet_B
    {
        protected override void OnHitTrigger(IDamageable damageable)
        {
            damageable.Damage(Damage, this.transform);
        }

    }

    public abstract class Bullet_B : MonoBehaviour, IBullet
    {
        LayerMask _excludedLayer;

        float _damage;
        protected float Damage { get; set; }

        public async void Init(float damage, LayerMask layer, float lifeTime)
        {
            _damage = damage;
            _excludedLayer = layer;
            Debug.Log(layer);
            await Pausable.PausableDestroy(gameObject, lifeTime);
        }

        void OnTriggerEnter(Collider other)
        {
            Debug.Log(other);
            if (other.gameObject.layer == _excludedLayer ||
                other.gameObject.layer == gameObject.layer) {  return; }

            if (other.TryGetComponent(out IDamageable damageable))
            {
                OnHitTrigger(damageable);
            }
            DeathBehavior(Damage);
            Debug.Log("hit");
            Destroy(this.gameObject);
        }
        protected virtual void DeathBehavior(float damage)
        {

        }
        protected abstract void OnHitTrigger(IDamageable damageable);
    }
}