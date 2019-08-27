using System;
using System.Collections;
using Game;
using UnityEngine;

namespace Game
{
    public class PipeSpawnPoint : MonoBehaviour
    {
        [SerializeField] private GameObject pipePrefab;
        [SerializeField] private uint spawnDelay = 2;

        private GameController _gameController;
        
        private void Awake()
        {
            _gameController = Finder.GameController;
        }

        private void OnEnable()
        {
            StartCoroutine(SpawnPipeRoutine());
        }

        private IEnumerator SpawnPipeRoutine()
        {
            while (isActiveAndEnabled)
            {
                if (_gameController.GameState == GameState.PLAYING)
                    Instantiate(pipePrefab, transform.position, Quaternion.identity);
                
                yield return new WaitForSeconds(spawnDelay);
            }
        }
    }
}