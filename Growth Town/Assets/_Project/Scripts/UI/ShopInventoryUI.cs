using UnityEngine;
using UnityEngine.UI;

namespace LifeTown.UI
{
    public class ShopInventoryUI : MonoBehaviour
    {
        [Header("UI References")]
        [SerializeField] private RectTransform uiPanel;
        [SerializeField] private Button closeButton;
        [SerializeField] private Button openButton;

        private float hiddenPositionY;
        private bool isOpen = false;

        private void Start()
        {
            if (uiPanel == null) return;
            hiddenPositionY = -uiPanel.rect.height;
            uiPanel.anchoredPosition = new Vector2(uiPanel.anchoredPosition.x, hiddenPositionY);

            if (closeButton != null) closeButton.onClick.AddListener(CloseUI);
            if (openButton != null) openButton.onClick.AddListener(OpenUI);
        }

        public void OpenUI()
        {
            if (isOpen || uiPanel == null) return;
            isOpen = true;
            uiPanel.gameObject.SetActive(true);
            uiPanel.anchoredPosition = Vector2.zero;
        }

        public void CloseUI()
        {
            if (!isOpen || uiPanel == null) return;
            isOpen = false;
            uiPanel.anchoredPosition = new Vector2(uiPanel.anchoredPosition.x, hiddenPositionY);
            uiPanel.gameObject.SetActive(false);
        }
    }
}
