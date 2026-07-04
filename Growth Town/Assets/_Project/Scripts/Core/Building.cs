using UnityEngine;
using LifeTown.UI;

namespace LifeTown.Core
{
    public class Building : MonoBehaviour
    {
        public int level = 1;

        private MergeAnimator mergeAnimator;

        private void Awake()
        {
            mergeAnimator = GetComponent<MergeAnimator>();
        }

        public void Upgrade(GameObject sourceBuilding)
        {
            // Destroy the source building
            Destroy(sourceBuilding);

            // Level up this building
            level++;
            
            // Visual feedback
            if (mergeAnimator != null)
            {
                mergeAnimator.PlayMergeAnimation();
            }

            Debug.Log($"[Building] Merged! Upgraded to Level {level}");

            // TODO: In a full game, here we would swap the mesh to the next tier's prefab.
            // For now, we scale it up slightly to show it leveled up.
            transform.localScale *= 1.1f;
        }

        public void Highlight(bool active)
        {
            // Simple visual highlight (jump slightly)
            if (active)
                transform.position += Vector3.up * 0.5f;
            else
                transform.position -= Vector3.up * 0.5f;
        }
    }
}
