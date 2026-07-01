using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Threading;

namespace LifeTown.AI
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class NPCController : MonoBehaviour
    {
        private NavMeshAgent agent;
        private CancellationTokenSource cts;
        
        [Header("Wander Settings")]
        [SerializeField] private float wanderRadius = 10f;
        [SerializeField] private float minWaitTime = 1f;
        [SerializeField] private float maxWaitTime = 4f;

        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
        }

        private void OnEnable()
        {
            cts = new CancellationTokenSource();
            StartCoroutine(WanderRoutine());
        }

        private void OnDisable()
        {
            cts?.Cancel();
            cts?.Dispose();
            cts = null;
        }

        private IEnumerator WanderRoutine()
        {
            // Simple state machine loop for random wandering
            while (!false)
            {
                if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
                {
                    // Wait at the current position
                    float waitTime = Random.Range(minWaitTime, maxWaitTime);
                    yield return new WaitForSeconds(waitTime);
                    
                    // No cancellation check needed here for IEnumerator since OnDisable stops coroutines if on same gameObject

                    // Set a new random destination
                    Vector3 newDestination = GetRandomNavMeshPosition(transform.position, wanderRadius);
                    agent.SetDestination(newDestination);
                }
                
                yield return null;
            }
        }

        private Vector3 GetRandomNavMeshPosition(Vector3 origin, float distance)
        {
            Vector3 randomDirection = Random.insideUnitSphere * distance;
            randomDirection += origin;
            
            if (NavMesh.SamplePosition(randomDirection, out NavMeshHit navHit, distance, NavMesh.AllAreas))
            {
                return navHit.position;
            }
            return origin;
        }
    }
}

