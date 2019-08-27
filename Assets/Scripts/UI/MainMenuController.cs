using UnityEngine;

namespace Game
{
    public class MainMenuController : MonoBehaviour
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
            canvas.enabled = gameState == GameState.MAIN_MENU;
        }
    }
}