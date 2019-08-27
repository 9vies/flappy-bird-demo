using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class HudController : MonoBehaviour
    {
        private Canvas canvas;
        private Text text;
        private GameController gameController;

        private void Awake()
        {
            canvas = GetComponent<Canvas>();
            text = GetComponentInChildren<Text>();
            gameController = Finder.GameController;
            UpdateVisibility(gameController.GameState);
        }

        private void OnEnable()
        {
            gameController.OnGameStateChanged += UpdateVisibility;
            gameController.OnGameScoreChanged += UpdateGameScore;

        }

        private void OnDisable()
        {
            gameController.OnGameStateChanged -= UpdateVisibility;
            gameController.OnGameScoreChanged -= UpdateGameScore;
        }

        private void UpdateVisibility(GameState gameState)
        {
            canvas.enabled = gameState == GameState.PLAYING;
        }

        private void UpdateGameScore(uint score)
        {
            text.text = score.ToString();
        }
    }
}