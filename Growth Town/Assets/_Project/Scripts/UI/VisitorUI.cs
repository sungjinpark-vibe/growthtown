using UnityEngine;
using UnityEngine.UI;

namespace LifeTown.UI
{
    public class VisitorUI : MonoBehaviour
    {
        [Header("UI References")]
        public GameObject tradePanel;
        public Text npcNameText;
        public Transform inventoryContainer;
        public Transform shopContainer;
        public Button confirmTradeButton;
        public Button leaveButton;

        private void Awake()
        {
            if (leaveButton != null)
                leaveButton.onClick.AddListener(CloseTrade);
                
            if (confirmTradeButton != null)
                confirmTradeButton.onClick.AddListener(ConfirmTrade);
        }

        public void OpenTrade(string npcName)
        {
            npcNameText.text = npcName;
            tradePanel.SetActive(true);
            RefreshTradeItems();
        }

        public void CloseTrade()
        {
            tradePanel.SetActive(false);
        }

        private void RefreshTradeItems()
        {
            // Logic to populate player inventory and NPC shop items
        }

        private void ConfirmTrade()
        {
            // Logic to execute the trade transaction
            Debug.Log("Trade confirmed with Raccoon NPC!");
        }
    }
}
