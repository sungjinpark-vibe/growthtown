using UnityEngine;
using System.Collections.Generic;
using System.IO;


public class CSVReader : MonoBehaviour
{
    [System.Serializable]
    public class NPCData
    {
        public int ID;
        public string Name;
        public float MoveSpeed;
        public string PreferredState;
    }

    public List<NPCData> npcDatabase = new List<NPCData>();
    
    public string csvFilePath = "Assets/_Project/Data/NPCBalance.csv";

    
    public void ParseNPCData()
    {
        npcDatabase.Clear();
        if (File.Exists(csvFilePath))
        {
            string[] lines = File.ReadAllLines(csvFilePath);
            for (int i = 1; i < lines.Length; i++) // Skip header
            {
                if (string.IsNullOrWhiteSpace(lines[i])) continue;

                string[] values = lines[i].Split(',');
                if (values.Length >= 4)
                {
                    NPCData data = new NPCData();
                    int.TryParse(values[0], out data.ID);
                    data.Name = values[1];
                    float.TryParse(values[2], out data.MoveSpeed);
                    data.PreferredState = values[3];
                    npcDatabase.Add(data);
                }
            }
            Debug.Log($"Successfully parsed {npcDatabase.Count} NPC records.");
        }
        else
        {
            Debug.LogError($"CSV file not found at {csvFilePath}");
        }
    }
}

