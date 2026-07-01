using UnityEngine;
using UnityEngine.AI;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace LifeTown.Art
{
    public class NPCPrefabSetup : MonoBehaviour
    {
#if UNITY_EDITOR
        [MenuItem("LifeTown/Art/Setup NPC Prefabs")]
        public static void SetupPrefabs()
        {
            CreateNPCPrefab("Cat_Dummy");
            CreateNPCPrefab("Dog_Dummy");
            CreateNPCPrefab("Parrot_Dummy");
        }

        private static void CreateNPCPrefab(string npcName)
        {
            GameObject npc = new GameObject(npcName);
            
            // Add required components
            NavMeshAgent agent = npc.AddComponent<NavMeshAgent>();
            
            // Configure agent based on type
            if (npcName == "Parrot_Dummy")
            {
                // Parrots waddle on the ground
                agent.speed = 1.5f;
                agent.height = 0.5f;
                agent.radius = 0.2f;
            }
            else if (npcName == "Dog_Dummy")
            {
                agent.speed = 3.5f;
                agent.height = 1.0f;
                agent.radius = 0.4f;
            }
            else if (npcName == "Cat_Dummy")
            {
                agent.speed = 3.0f;
                agent.height = 0.8f;
                agent.radius = 0.3f;
            }

            // Create Visual child
            GameObject visual = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            visual.name = "Visual";
            visual.transform.SetParent(npc.transform);
            visual.transform.localPosition = new Vector3(0, agent.height / 2, 0);
            visual.transform.localScale = new Vector3(agent.radius * 2, agent.height / 2, agent.radius * 2);

            // Destroy the default collider from primitive
            DestroyImmediate(visual.GetComponent<Collider>());

            Debug.Log($"Setup completed for {npcName}");
        }
#endif
    }
}
