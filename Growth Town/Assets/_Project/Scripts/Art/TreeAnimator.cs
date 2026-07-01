using UnityEngine;

namespace LifeTown.Art
{
    public class TreeAnimator : MonoBehaviour
    {
        [Header("Animation Settings")]
        [SerializeField] private float animationDuration = 0.5f;
        
        private Vector3 originalScale;

        private void Awake()
        {
            originalScale = transform.localScale;
        }

        private void OnEnable()
        {
            AnimateGrowth();
        }

        public void AnimateGrowth()
        {
            transform.localScale = originalScale;
        }
    }
}
