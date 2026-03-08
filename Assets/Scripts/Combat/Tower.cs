using Tafang.Core;
using Tafang.Enemies;
using UnityEngine;

namespace Tafang.Combat
{
    public class Tower : MonoBehaviour
    {
        [Header("Attack")]
        [SerializeField] private float attackRange = 2.5f;
        [SerializeField] private float fireRate = 1f;
        [SerializeField] private int damage = 1;
        [SerializeField] private LayerMask enemyLayers;

        [Header("Prefabs")]
        [SerializeField] private Bullet bulletPrefab;
        [SerializeField] private Transform muzzle;

        [Header("View")]
        [SerializeField] private bool rotateToTarget = true;

        private EnemyHealth currentTarget;
        private float fireCooldown;

        private void Update()
        {
            if (GameManager.Instance != null && GameManager.Instance.IsGameOver)
            {
                return;
            }

            if (currentTarget == null || !IsInRange(currentTarget.transform.position))
            {
                currentTarget = AcquireTarget();
            }

            if (currentTarget == null)
            {
                return;
            }

            if (rotateToTarget)
            {
                RotateTowards(currentTarget.transform.position);
            }

            fireCooldown -= Time.deltaTime;
            if (fireCooldown <= 0f)
            {
                Shoot(currentTarget);
                fireCooldown = 1f / Mathf.Max(0.01f, fireRate);
            }
        }

        private EnemyHealth AcquireTarget()
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, attackRange, enemyLayers);
            if (hits == null || hits.Length == 0)
            {
                return null;
            }

            EnemyHealth bestTarget = null;
            float bestDistance = float.MaxValue;

            foreach (Collider2D hit in hits)
            {
                EnemyHealth enemy = hit.GetComponent<EnemyHealth>();
                if (enemy == null)
                {
                    continue;
                }

                float dist = (enemy.transform.position - transform.position).sqrMagnitude;
                if (dist < bestDistance)
                {
                    bestDistance = dist;
                    bestTarget = enemy;
                }
            }

            return bestTarget;
        }

        private void Shoot(EnemyHealth target)
        {
            if (bulletPrefab == null || target == null)
            {
                return;
            }

            Transform launchPoint = muzzle != null ? muzzle : transform;
            Bullet bullet = Instantiate(bulletPrefab, launchPoint.position, Quaternion.identity);
            bullet.Initialize(target, damage);
        }

        private bool IsInRange(Vector3 targetPosition)
        {
            float sqrDistance = (targetPosition - transform.position).sqrMagnitude;
            return sqrDistance <= attackRange * attackRange;
        }

        private void RotateTowards(Vector3 targetPosition)
        {
            Vector2 direction = targetPosition - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(transform.position, attackRange);
        }
    }
}

