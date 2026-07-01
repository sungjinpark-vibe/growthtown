using System;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance { get; private set; }
    
    public bool IsGoldFeverActive { get; private set; }
    
    [System.Serializable]
    public class FeverTimeData
    {
        public int startHour;
        public int endHour;
    }
    
    private List<FeverTimeData> feverTimes = new List<FeverTimeData>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadFeverTimeData();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        CheckFeverTime();
    }

    private void LoadFeverTimeData()
    {
        TextAsset csvFile = Resources.Load<TextAsset>("FeverTimeBalance");
        if (csvFile != null)
        {
            string[] lines = csvFile.text.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            // Assume format: StartHour,EndHour
            for (int i = 1; i < lines.Length; i++) // Skip header
            {
                string[] values = lines[i].Split(',');
                if (values.Length >= 2 && int.TryParse(values[0], out int start) && int.TryParse(values[1], out int end))
                {
                    feverTimes.Add(new FeverTimeData { startHour = start, endHour = end });
                }
            }
        }
        else
        {
            Debug.LogWarning("FeverTimeBalance.csv not found in Resources!");
        }
    }

    private void CheckFeverTime()
    {
        int currentHour = DateTime.Now.Hour;
        bool isFever = false;
        
        foreach (var data in feverTimes)
        {
            if (currentHour >= data.startHour && currentHour < data.endHour)
            {
                isFever = true;
                break;
            }
        }
        
        if (IsGoldFeverActive != isFever)
        {
            IsGoldFeverActive = isFever;
            Debug.Log($"Gold Fever Event is now {(IsGoldFeverActive ? "Active" : "Inactive")}");
        }
    }
}
