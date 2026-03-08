using System;
using UnityEngine;

namespace Tafang.Core
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        [Header("Starting Resources")]
        [SerializeField] private int startGold = 150;
        [SerializeField] private int startLives = 20;

        public int Gold { get; private set; }
        public int Lives { get; private set; }
        public int CurrentWave { get; private set; }
        public bool IsGameOver { get; private set; }

        public event Action<int> GoldChanged;
        public event Action<int> LivesChanged;
        public event Action<int> WaveChanged;
        public event Action<bool> GameOverChanged;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            Gold = startGold;
            Lives = startLives;
            CurrentWave = 0;
            IsGameOver = false;
        }

        private void Start()
        {
            GoldChanged?.Invoke(Gold);
            LivesChanged?.Invoke(Lives);
            WaveChanged?.Invoke(CurrentWave);
            GameOverChanged?.Invoke(IsGameOver);
        }

        public bool SpendGold(int amount)
        {
            if (amount <= 0)
            {
                return true;
            }

            if (Gold < amount || IsGameOver)
            {
                return false;
            }

            Gold -= amount;
            GoldChanged?.Invoke(Gold);
            return true;
        }

        public void AddGold(int amount)
        {
            if (amount <= 0 || IsGameOver)
            {
                return;
            }

            Gold += amount;
            GoldChanged?.Invoke(Gold);
        }

        public void DamageBase(int damage)
        {
            if (damage <= 0 || IsGameOver)
            {
                return;
            }

            Lives = Mathf.Max(0, Lives - damage);
            LivesChanged?.Invoke(Lives);

            if (Lives == 0)
            {
                SetGameOver(true);
            }
        }

        public void SetWave(int waveNumber)
        {
            CurrentWave = Mathf.Max(0, waveNumber);
            WaveChanged?.Invoke(CurrentWave);
        }

        public void SetGameOver(bool value)
        {
            if (IsGameOver == value)
            {
                return;
            }

            IsGameOver = value;
            GameOverChanged?.Invoke(IsGameOver);
        }
    }
}

