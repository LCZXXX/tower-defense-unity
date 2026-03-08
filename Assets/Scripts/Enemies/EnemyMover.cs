using Tafang.Core;
using Tafang.Pathing;
using UnityEngine;

namespace Tafang.Enemies
{
    [RequireComponent(typeof(EnemyHealth))]
    public class EnemyMover : MonoBehaviour
    {
        [SerializeField] private WaypointPath path;
        [SerializeField] private float moveSpeed = 2f;
        [SerializeField] private int leakDamage = 1;
        [SerializeField] private float reachedDistance = 0.05f;

        private EnemyHealth enemyHealth;
        private int waypointIndex;
        public float MoveSpeed
        {
            get { return moveSpeed; }
        }

        private void Awake()
        {
            enemyHealth = GetComponent<EnemyHealth>();
        }

        private void Start()
        {
            if (path == null || path.Count == 0)
            {
                Debug.LogError("EnemyMover requires a valid WaypointPath.", this);
                enabled = false;
                return;
            }

            transform.position = path.GetWaypoint(0).position;
            waypointIndex = 1;
        }

        private void Update()
        {
            if (waypointIndex >= path.Count)
            {
                return;
            }

            Transform targetPoint = path.GetWaypoint(waypointIndex);
            transform.position = Vector3.MoveTowards(
                transform.position,
                targetPoint.position,
                moveSpeed * Time.deltaTime);

            float sqrDistance = (transform.position - targetPoint.position).sqrMagnitude;
            if (sqrDistance <= reachedDistance * reachedDistance)
            {
                waypointIndex++;
                if (waypointIndex >= path.Count)
                {
                    ReachGoal();
                }
            }
        }

        public void SetPath(WaypointPath value)
        {
            path = value;
        }

        public void SetMoveSpeed(float speed)
        {
            moveSpeed = Mathf.Max(0f, speed);
        }

        private void ReachGoal()
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.DamageBase(leakDamage);
            }

            enemyHealth.LeakAndDie();
        }
    }
}
