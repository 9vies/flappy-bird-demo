using System;
using Events;
using Game;
using Sensors;
using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(PlayerMover))]
    public class Player : MonoBehaviour
    {
        public const string PLAYER_TAG = "Player";
        
        [SerializeField] private float targetMainMenuHeight = 0f;
        
        private GameController gameController;
        private PlayerMover playerMover;
        private PlayerDeathEventChannel playerDeathEventChannel;
        private HazardSensor hazardSensor;
        

        private void Awake()
        {
            gameController = Finder.GameController;
            playerDeathEventChannel = Finder.PlayerDeathEventChannel;
            playerMover = GetComponent<PlayerMover>();
            hazardSensor = GetComponentInChildren<HazardSensor>();
        }

        private void OnEnable()
        {
            hazardSensor.OnHit += OnHit;
        }

        private void OnDisable()
        {
            hazardSensor.OnHit -= OnHit;
        }

        private void Update()
        {
            var gameState = gameController.GameState;

            if (gameState == GameState.MAIN_MENU)
            {
                if (transform.position.y < targetMainMenuHeight) 
                    playerMover.Flap();
            }
            else if (gameState == GameState.PLAYING)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                    playerMover.Flap();
            }
        }

        private void OnHit(GameObject other)
        {
            Die();
        }

        [ContextMenu("Die")]
        private void Die()
        {
            playerDeathEventChannel.NotifyPlayerDeath();
            Destroy(gameObject);
        }
    }
}