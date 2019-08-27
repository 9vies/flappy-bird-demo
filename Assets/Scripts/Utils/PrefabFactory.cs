using UnityEngine;

namespace Game
{
    public class PrefabFactory : MonoBehaviour
    {
        [SerializeField] private GameObject pipePairPrefab;

        public GameObject CreatePipePair()
        {
            return CreatePipePair(Vector3.zero, Quaternion.identity, null);
        }

        public GameObject CreatePipePair(Vector3 position, Quaternion rotation, GameObject parent)
        {
            var stupidNameForAVariableAmIRight = Instantiate(this.pipePairPrefab, position, rotation);
            if (parent)
                stupidNameForAVariableAmIRight.transform.parent = parent.transform;

            return stupidNameForAVariableAmIRight;
        }
    }
}