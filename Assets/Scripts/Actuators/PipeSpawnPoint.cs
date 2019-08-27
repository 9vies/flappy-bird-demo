using System;
using System.Collections;
using Game;
using UnityEngine;

namespace Game
{
    public class PipeSpawnPoint : MonoBehaviour
    {
        [SerializeField] private uint spawnDelay = 2;

        private PrefabFactory prefabFactory;
        private GameController gameController;
        
        private void Awake()
        {
            gameController = Finder.GameController;
            prefabFactory = Finder.PrefabFactory;
        }

        private void OnEnable()
        {
            StartCoroutine(SpawnPipeRoutine());
        }

        private IEnumerator SpawnPipeRoutine()
        {
            while (isActiveAndEnabled)
            {
                if (gameController.GameState == GameState.PLAYING)
                    prefabFactory.CreatePipePair(transform.position, Quaternion.identity, null);
                
                yield return new WaitForSeconds(spawnDelay);
            }
        }
    }
}