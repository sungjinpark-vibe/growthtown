using UnityEngine;
using Unity.AI.Navigation;
using System.Collections;

namespace LifeTown.AI
{
    public class NavMeshManager : MonoBehaviour
    {
        [SerializeField] private NavMeshSurface navMeshSurface;

        public void RebakeNavMeshAsync() { StartCoroutine(RebakeNavMeshCoroutine()); }
        
        private IEnumerator RebakeNavMeshCoroutine()
        {
            if (navMeshSurface == null)
            {
                Debug.LogWarning("NavMeshSurface is not assigned.");
                yield break;
            }
            
            // Re-bake NavMesh asynchronously using IEnumerator to avoid blocking the main thread completely
            var asyncOp = navMeshSurface.UpdateNavMesh(navMeshSurface.navMeshData);
            yield return asyncOp;
            
            Debug.Log("NavMesh rebake completed.");
        }
    }
}

