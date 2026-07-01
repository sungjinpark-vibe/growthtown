using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace LifeTown.UI
{
    public class QuestUI : MonoBehaviour
    {
        [Header("UI References")]
        public GameObject modalPanel;
        public Transform questListContainer;
        public GameObject questItemPrefab;
        public Button closeButton;

        private void Awake()
        {
            if (closeButton != null)
                closeButton.onClick.AddListener(CloseModal);
        }

        public void OpenModal(List<string> dailyQuests)
        {
            modalPanel.SetActive(true);
            PopulateQuests(dailyQuests);
        }

        public void CloseModal()
        {
            modalPanel.SetActive(false);
        }

        private void PopulateQuests(List<string> quests)
        {
            // Clear existing
            foreach (Transform child in questListContainer)
            {
                Destroy(child.gameObject);
            }

            // Populate new
            foreach (var quest in quests)
            {
                GameObject questItem = Instantiate(questItemPrefab, questListContainer);
                // Setup quest item UI (e.g., text, icon) based on your prefab
            }
        }
    }
}
