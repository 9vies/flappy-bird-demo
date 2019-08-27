using System;
using Game;
using UnityEngine;

namespace Game
{
    public class GameOverMenuController : MonoBehaviour
    {
        private Canvas canvas;
        private GameController gameController;

        private void Awake()
        {
            canvas = GetComponent<Canvas>();
            gameController = Finder.GameController;
            UpdateVisibility(gameController.GameState);
        }

        private void OnEnable()
        {
            gameController.OnGameStateChanged += UpdateVisibility;
        }

        private void OnDisable()
        {
            gameController.OnGameStateChanged -= UpdateVisibility;
        }

        private void UpdateVisibility(GameState gameState)
        {
            canvas.enabled = gameState == GameState.GAME_OVER;
        }
    }
}