using UnityEngine;
using System;

namespace PowerTycoon.Core
{
    /// <summary>
    /// Hauptmanager für das Spiel. Verwaltet Spielzustand und zentrale Systeme.
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        [SerializeField] private float gameSpeed = 1f;
        private bool isPaused = false;

        // Events
        public event Action OnGameStarted;
        public event Action OnGamePaused;
        public event Action OnGameResumed;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            InitializeGame();
        }

        private void InitializeGame()
        {
            Debug.Log("PowerTycoon: Spiel wird initialisiert...");
            OnGameStarted?.Invoke();
        }

        private void Update()
        {
            // Pausieren mit Leertaste (später UI-Button)
            if (Input.GetKeyDown(KeyCode.Space))
            {
                TogglePause();
            }
        }

        public void TogglePause()
        {
            isPaused = !isPaused;
            Time.timeScale = isPaused ? 0f : 1f;

            if (isPaused)
            {
                OnGamePaused?.Invoke();
            }
            else
            {
                OnGameResumed?.Invoke();
            }
        }

        public void SetGameSpeed(float speed)
        {
            gameSpeed = Mathf.Clamp(speed, 0.1f, 5f);
            Time.timeScale = isPaused ? 0f : gameSpeed;
        }

        public float GetGameSpeed() => gameSpeed;
        public bool IsPaused() => isPaused;
    }
}
