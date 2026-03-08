using Tafang.Core;
using UnityEngine;

namespace Tafang.Enemies
{
    public class EnemyHealth : MonoBehaviour
    {
        public static int ActiveEnemies { get; private set; }

        [SerializeField] private int maxHealth = 5;
        [SerializeField] private int rewardGold = 10;
        [SerializeField] private bool destroyOnDeath = true;

        private int currentHealth;
        private bool isDead;

        private void OnEnable()
        {
            ActiveEnemies++;
            currentHealth = maxHealth;
            isDead = false;
        }

        private void OnDisable()
        {
            ActiveEnemies = Mathf.Max(0, ActiveEnemies - 1);
        }

        public void TakeDamage(int damage)
        {
            if (isDead || damage <= 0)
            {
                return;
            }

            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                Die(true);
            }
        }

        public void LeakAndDie()
        {
            if (isDead)
            {
                return;
            }

            Die(false);
        }

        private void Die(bool grantReward)
        {
            isDead = true;

            if (grantReward && GameManager.Instance != null)
            {
                GameManager.Instance.AddGold(rewardGold);
            }

            if (destroyOnDeath)
            {
                Destroy(gameObject);
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }
}

