using System;
using System.Collections;
using Events;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private KeyCode startGameKey = KeyCode.Space;

        private GameState gameState = GameState.MAIN_MENU;

        private PlayerDeathEventChannel playerDeathEventChannel;
        private ScoreEventChannel scoreEventChannel;

        private uint score;

        public uint Score
        {
            get => score;
            private set
            {
                if (score != value)
                {
                    score = value;
                    NotifyGameScoreChanged();
                }
            }
        }

        public GameState GameState
        {
            get => gameState;
            private set
            {
                if (gameState != value)
                {
                    gameState = value;
                    NotifyGameStatedChanged();
                }
            }
        }

        public event GameStateChangedEventHandler OnGameStateChanged;

        public event ScoreChangedEventHandler OnGameScoreChanged;

        private void Awake()
        {
            playerDeathEventChannel = Finder.PlayerDeathEventChannel;
            scoreEventChannel = Finder.ScoreEventChannel;
            score = 0;
        }

        private void Start()
        {
            if (!SceneManager.GetSceneByName(Scenes.Game).isLoaded)
                StartCoroutine(LoadGame());
            else
                SceneManager.SetActiveScene(SceneManager.GetSceneByName(Scenes.Game));
        }

        private IEnumerator LoadGame()
        {
            yield return SceneManager.LoadSceneAsync(Scenes.Game, LoadSceneMode.Additive);
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(Scenes.Game));
        }

        private IEnumerator UnloadGame()
        {
            yield return SceneManager.UnloadSceneAsync(Scenes.Game);
        }

        private IEnumerator ReloadGame()
        {
            yield return UnloadGame();
            yield return LoadGame();
        }

        private void OnEnable()
        {
            playerDeathEventChannel.OnPlayerDeath += EndGame;
            scoreEventChannel.OnScoreChanged += IncrementScore;
        }

        private void OnDisable()
        {
            playerDeathEventChannel.OnPlayerDeath -= EndGame;
            scoreEventChannel.OnScoreChanged -= IncrementScore;
        }

        private void Update()
        {
            if (GameState == GameState.MAIN_MENU && Input.GetKeyDown(startGameKey))
                GameState = GameState.PLAYING;

            if (GameState == GameState.GAME_OVER && Input.GetKeyDown(startGameKey))
                RestartGame();
        }

        private void NotifyGameStatedChanged()
        {
            if (OnGameStateChanged != null)
                OnGameStateChanged(gameState);
        }

        private void NotifyGameScoreChanged()
        {
            if (OnGameScoreChanged != null)
                OnGameScoreChanged(score);
        }

        private void EndGame()
        {
            GameState = GameState.GAME_OVER;
        }

        private void IncrementScore()
        {
            Score++;
        }

        private void RestartGame()
        {
            GameState = GameState.MAIN_MENU;
            Score = 0;

            StartCoroutine(ReloadGame());
        }
    }

    public delegate void GameStateChangedEventHandler(GameState newGameState);

    public delegate void ScoreChangedEventHandler(uint score);

    public enum GameState
    {
        MAIN_MENU,
        PLAYING,
        GAME_OVER
    }
}