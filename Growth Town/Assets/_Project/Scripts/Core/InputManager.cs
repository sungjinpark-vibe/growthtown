using UnityEngine;
using UnityEngine.EventSystems;

namespace LifeTown.Core
{
    public class InputManager : MonoBehaviour
    {
        private Building selectedBuilding;

        private void Update()
        {
            // Check for mouse click (or touch)
            if (Input.GetMouseButtonDown(0))
            {
                HandleClick();
            }
        }

        private void HandleClick()
        {
            // Ignore UI clicks
            if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject()) 
                return;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Building clickedBuilding = hit.collider.GetComponentInParent<Building>();
                
                if (clickedBuilding != null)
                {
                    Debug.Log($"[InputManager] Clicked Building Lv {clickedBuilding.level}");

                    if (selectedBuilding == null)
                    {
                        // Select first building
                        selectedBuilding = clickedBuilding;
                        selectedBuilding.Highlight(true);
                    }
                    else
                    {
                        if (selectedBuilding == clickedBuilding)
                        {
                            // Deselect if clicking the same building again
                            selectedBuilding.Highlight(false);
                            selectedBuilding = null;
                        }
                        else if (selectedBuilding.level == clickedBuilding.level)
                        {
                            // Merge!
                            selectedBuilding.Highlight(false);
                            clickedBuilding.Upgrade(selectedBuilding.gameObject);
                            selectedBuilding = null; // Source is destroyed
                        }
                        else
                        {
                            // Change selection
                            selectedBuilding.Highlight(false);
                            selectedBuilding = clickedBuilding;
                            selectedBuilding.Highlight(true);
                        }
                    }
                }
                else
                {
                    // Clicked empty ground
                    if (selectedBuilding != null)
                    {
                        selectedBuilding.Highlight(false);
                        selectedBuilding = null;
                    }
                }
            }
        }
    }
}
