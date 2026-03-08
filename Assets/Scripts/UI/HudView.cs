using Tafang.Core;
using Tafang.Spawning;
using TMPro;
using UnityEngine;

namespace Tafang.UI
{
    public class HudView : MonoBehaviour
    {
        [SerializeField] private TMP_Text goldText;
        [SerializeField] private TMP_Text livesText;
        [SerializeField] private TMP_Text waveText;
        [SerializeField] private TMP_Text statusText;
        [SerializeField] private WaveSpawner waveSpawner;

        private GameManager gameManager;

        private void Start()
        {
            gameManager = GameManager.Instance;
            if (gameManager == null)
            {
                Debug.LogError("HudView requires GameManager in scene.");
                enabled = false;
                return;
            }

            gameManager.GoldChanged += OnGoldChanged;
            gameManager.LivesChanged += OnLivesChanged;
            gameManager.WaveChanged += OnWaveChanged;
            gameManager.GameOverChanged += OnGameOverChanged;

            OnGoldChanged(gameManager.Gold);
            OnLivesChanged(gameManager.Lives);
            OnWaveChanged(gameManager.CurrentWave);
            OnGameOverChanged(gameManager.IsGameOver);
        }

        private void OnDestroy()
        {
            if (gameManager == null)
            {
                return;
            }

            gameManager.GoldChanged -= OnGoldChanged;
            gameManager.LivesChanged -= OnLivesChanged;
            gameManager.WaveChanged -= OnWaveChanged;
            gameManager.GameOverChanged -= OnGameOverChanged;
        }

        private void Update()
        {
            if (statusText == null || waveSpawner == null || gameManager == null)
            {
                return;
            }

            if (!gameManager.IsGameOver && waveSpawner.AllWavesCompleted)
            {
                statusText.text = "VICTORY";
            }
        }

        private void OnGoldChanged(int value)
        {
            if (goldText != null)
            {
                goldText.text = "Gold: " + value;
            }
        }

        private void OnLivesChanged(int value)
        {
            if (livesText != null)
            {
                livesText.text = "Lives: " + value;
            }
        }

        private void OnWaveChanged(int value)
        {
            if (waveText != null)
            {
                waveText.text = "Wave: " + value;
            }
        }

        private void OnGameOverChanged(bool isGameOver)
        {
            if (statusText != null)
            {
                statusText.text = isGameOver ? "GAME OVER" : string.Empty;
            }
        }
    }
}

