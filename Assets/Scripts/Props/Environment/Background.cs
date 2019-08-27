using UnityEngine;

namespace Game
{
    public class Background : MonoBehaviour
    {
        [Header("Visuals")] [SerializeField] private Sprite sprite = null;

        [Range(0, 10)] [SerializeField] private uint nbTiles = 3;

        [SerializeField] private string sortingLayerName = "Default";

        [Header("Behaviour")] [Range(0, 100)] [SerializeField]
        private float backgroundSpeed = 0.5f;

        private Vector2 tileSize;
        private Vector3 initialPosition;

        private GameController gameController;
        
        private float offset;

        private void Awake()
        {
            gameController = Finder.GameController;
        }

        private void Start()
        {
            tileSize = sprite.bounds.size;
            for (int i = 0; i < nbTiles; ++i)
            {
                var tile = new GameObject(i.ToString());
                tile.transform.parent = transform;
                tile.transform.localPosition = tileSize.x * i * Vector3.right;

                var spriteRenderer = tile.AddComponent<SpriteRenderer>();
                spriteRenderer.sprite = sprite;
                spriteRenderer.sortingLayerName = sortingLayerName;
            }

            initialPosition = transform.position;
            offset = 0;
        }

        private void Update()
        {
            if (gameController.GameState != GameState.GAME_OVER)
                RepositionBackground();
        }

        private void RepositionBackground()
        {
            offset = (offset + (Time.deltaTime * backgroundSpeed)) % tileSize.x;

            transform.position = initialPosition + Vector3.left * offset;
        }
#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            var size = sprite == null ? Vector3.one : sprite.bounds.size;
            var center = transform.position;

            Gizmos.DrawWireCube(center, size);
        }
    }
#endif
}