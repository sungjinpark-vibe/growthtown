using UnityEngine;
using UnityEngine.Rendering;

namespace LifeTown.Rendering
{
    [RequireComponent(typeof(SortingGroup))]
    public class SpriteSorter : MonoBehaviour
    {
        [SerializeField] private float offset = 0f;
        [SerializeField] private bool runOnlyOnce = false;

        private SortingGroup sortingGroup;

        private void Awake()
        {
            sortingGroup = GetComponent<SortingGroup>();
        }

        private void LateUpdate()
        {
            UpdateSortingOrder();

            if (runOnlyOnce)
            {
                Destroy(this);
            }
        }

        private void UpdateSortingOrder()
        {
            // Calculate sorting order based on y position
            // The lower the y position, the higher the sorting order (rendered on top)
            if (sortingGroup != null)
            {
                sortingGroup.sortingOrder = (int)(-(transform.position.y + offset) * 100f);
            }
        }
    }
}
