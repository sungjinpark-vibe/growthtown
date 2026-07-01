using UnityEngine;

namespace LifeTown.Buildings
{
    /// <summary>
    /// Placeholder class for dummy building prefabs.
    /// Used for layout testing before final 3D assets are ready.
    /// </summary>
    public class BuildingPlaceholder : MonoBehaviour
    {
        [Header("Placeholder Data")]
        [SerializeField] private string buildingName = "Dummy Building";
        [SerializeField] private Vector2Int gridSize = new Vector2Int(2, 2);

        private void Start()
        {
            Debug.Log($"[BuildingPlaceholder] Initialized {buildingName} taking up {gridSize.x}x{gridSize.y} grid cells.");
        }

        public void Interact()
        {
            Debug.Log($"[BuildingPlaceholder] Interacted with {buildingName}.");
        }
    }
}
