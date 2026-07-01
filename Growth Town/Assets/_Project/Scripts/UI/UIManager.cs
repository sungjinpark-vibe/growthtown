using UnityEngine;
using UnityEngine.UI;

namespace LifeTown.UI
{
    /// <summary>
    /// Manages the primary UI layout as per PRD: Top 10% header, and bottom 72dp FAB.
    /// </summary>
    public class UIManager : MonoBehaviour
    {
        [Header("UI References")]
        [SerializeField] private RectTransform headerPanel;
        [SerializeField] private RectTransform fabButton;

        private void Awake()
        {
            SetupUILayout();
        }

        private void SetupUILayout()
        {
            // Configure Header (Top 10%)
            if (headerPanel != null)
            {
                headerPanel.anchorMin = new Vector2(0, 0.9f);
                headerPanel.anchorMax = new Vector2(1, 1f);
                headerPanel.offsetMin = Vector2.zero; // Left, Bottom
                headerPanel.offsetMax = Vector2.zero; // Right, Top
            }

            // Configure FAB (72dp size, positioned bottom right)
            if (fabButton != null)
            {
                fabButton.anchorMin = new Vector2(1, 0);
                fabButton.anchorMax = new Vector2(1, 0);
                fabButton.sizeDelta = new Vector2(72f, 72f);
                
                // Positioned 24dp away from bottom-right corner
                fabButton.anchoredPosition = new Vector2(-60f, 60f); // 24 + 72/2 = 60
            }
        }
    }
}
