using System;
using System.Collections.Generic;
using UnityEngine;

public class ShopItem
{
    public string id;
    public string name;
    public string type; // e.g. Building, NPC
    public int price;
}

public class ShopManager : MonoBehaviour
{
    public List<ShopItem> allItems = new List<ShopItem>();
    public List<ShopItem> currentShopItems = new List<ShopItem>();
    
    public float rotationIntervalSeconds = 3600f; // 1 hour
    private float timeSinceLastRotation = 0f;

    void Start()
    {
        LoadShopItems();
        RotateShopItems();
    }

    void Update()
    {
        timeSinceLastRotation += Time.deltaTime;
        if (timeSinceLastRotation >= rotationIntervalSeconds)
        {
            RotateShopItems();
            timeSinceLastRotation = 0f;
        }
    }

    private void LoadShopItems()
    {
        TextAsset csvData = Resources.Load<TextAsset>("ShopItemList");
        if (csvData != null)
        {
            string[] lines = csvData.text.Split('\n');
            for (int i = 1; i < lines.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(lines[i])) continue;
                string[] fields = lines[i].Split(',');
                if (fields.Length >= 4)
                {
                    ShopItem item = new ShopItem
                    {
                        id = fields[0].Trim(),
                        name = fields[1].Trim(),
                        type = fields[2].Trim(),
                        price = int.TryParse(fields[3].Trim(), out int p) ? p : 0
                    };
                    allItems.Add(item);
                }
            }
        }
    }

    public void RotateShopItems()
    {
        currentShopItems.Clear();
        if (allItems.Count == 0) return;

        List<ShopItem> tempItems = new List<ShopItem>(allItems);
        for (int i = 0; i < 5; i++)
        {
            if (tempItems.Count == 0) break;
            int randomIndex = UnityEngine.Random.Range(0, tempItems.Count);
            currentShopItems.Add(tempItems[randomIndex]);
            tempItems.RemoveAt(randomIndex);
        }
    }
}
