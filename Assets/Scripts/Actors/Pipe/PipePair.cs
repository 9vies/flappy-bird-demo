using System;
using System.Collections;
using Events;
using Sensors;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game
{
    [RequireComponent(typeof(TranslateMover))]
    public class PipePair : MonoBehaviour
    {
        [Header("Behaviour")] [SerializeField] private float delayInSeconds = 6f;
        [Header("Position")] [SerializeField] private float minY = -1f;
        [SerializeField] private float maxY = 1f;

        private ScoreEventChannel scoreEventChannel;
        private PlayerSensor playerSensor;
        private TranslateMover translateMover;
        private GameController gameController;

        private void Awake()
        {
            playerSensor = GetComponentInChildren<PlayerSensor>();
            translateMover = GetComponent<TranslateMover>();
            gameController = Finder.GameController;
            scoreEventChannel = Finder.ScoreEventChannel;
        }

        private void OnEnable()
        {
            StartCoroutine(DestroyPipes());
            playerSensor.OnHit += OnHit;
        }

        private void OnDisable()
        {
            playerSensor.OnHit -= OnHit;
        }

        private void Start()
        {
            transform.Translate(Vector3.up * Random.Range(minY, maxY));
        }
        
        public void Update()
        {
            var gameState = gameController.GameState;

            if (gameState == GameState.PLAYING)
            {
                translateMover.Move();
            }
        }
        
        private void OnHit(GameObject other)
        {
            scoreEventChannel.NotifyScoreChanged();
        }
        
        private IEnumerator DestroyPipes()
        {
            yield return new WaitForSeconds(delayInSeconds);
            if (gameController.GameState == GameState.PLAYING)
                Destroy(gameObject);
        }
    }
}