using UnityEngine;

namespace LifeTown.UI
{
    public class MergeAnimator : MonoBehaviour
    {
        [Header("Merge Bounce Settings")]
        public float bounceScale = 1.3f;
        public float duration = 0.5f;
        public int vibrato = 5;
        
        [Range(0f, 1f)]
        public float elasticity = 1f;

        private Vector3 originalScale;

        private void Awake()
        {
            originalScale = transform.localScale;
        }

        public void PlayMergeAnimation()
        {
            transform.localScale = originalScale;
        }
    }
}
