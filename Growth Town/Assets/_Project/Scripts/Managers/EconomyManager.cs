using System.Collections.Generic;
using UnityEngine;

public class Building
{
    public string id;
    public int goldProductionRate; // Amount of gold produced per cycle
    public float productionTickTime; // Time in seconds between production cycles
    public int currentGoldStored;
    public int maxGoldStored;
    public float timeSinceLastProduction;
}

public class EconomyManager : MonoBehaviour
{
    public int totalGold = 0;
    public List<Building> activeBuildings = new List<Building>();

    void Update()
    {
        ProduceGold();
    }

    private void ProduceGold()
    {
        foreach (Building building in activeBuildings)
        {
            building.timeSinceLastProduction += Time.deltaTime;
            
            if (building.timeSinceLastProduction >= building.productionTickTime)
            {
                if (building.currentGoldStored < building.maxGoldStored)
                {
                    building.currentGoldStored += building.goldProductionRate;
                    if (building.currentGoldStored > building.maxGoldStored)
                    {
                        building.currentGoldStored = building.maxGoldStored;
                    }
                }
                building.timeSinceLastProduction = 0f;
            }
        }
    }

    public void HarvestGold(Building building)
    {
        if (activeBuildings.Contains(building) && building.currentGoldStored > 0)
        {
            totalGold += building.currentGoldStored;
            building.currentGoldStored = 0;
            Debug.Log($"Harvested gold. Total gold: {totalGold}");
        }
    }

    public void AddBuilding(Building building)
    {
        activeBuildings.Add(building);
    }

    public void RemoveBuilding(Building building)
    {
        activeBuildings.Remove(building);
    }
}
