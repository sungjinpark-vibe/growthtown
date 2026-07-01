using UnityEngine;

public class VisitorManager : MonoBehaviour
{
    public static VisitorManager Instance { get; private set; }

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

    public void SpawnVisitor()
    {
        // TODO: Implement visitor spawning logic
        Debug.Log("Visitor spawned.");
    }

    public void RemoveVisitor(GameObject visitor)
    {
        // TODO: Implement visitor removal logic
        Debug.Log("Visitor removed.");
    }
}
