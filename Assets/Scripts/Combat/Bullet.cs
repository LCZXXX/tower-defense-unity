using Tafang.Enemies;
using UnityEngine;

namespace Tafang.Combat
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 10f;
        [SerializeField] private float hitRadius = 0.1f;
        [SerializeField] private float lifeTime = 3f;

        private EnemyHealth target;
        private int damage;

        public void Initialize(EnemyHealth targetEnemy, int damageValue)
        {
            target = targetEnemy;
            damage = damageValue;
            Destroy(gameObject, lifeTime);
        }

        private void Update()
        {
            if (target == null)
            {
                Destroy(gameObject);
                return;
            }

            Vector3 targetPosition = target.transform.position;
            transform.position = Vector3.MoveTowards(
                transform.position,
                targetPosition,
                moveSpeed * Time.deltaTime);

            float sqrDistance = (transform.position - targetPosition).sqrMagnitude;
            if (sqrDistance <= hitRadius * hitRadius)
            {
                target.TakeDamage(damage);
                Destroy(gameObject);
            }
        }
    }
}

