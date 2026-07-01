using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartQuest(string questId)
    {
        // TODO: Implement starting a quest
        Debug.Log($"Quest {questId} started.");
    }

    public void CompleteQuest(string questId)
    {
        // TODO: Implement completing a quest
        Debug.Log($"Quest {questId} completed.");
    }
}
