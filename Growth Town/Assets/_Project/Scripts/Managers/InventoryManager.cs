using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public List<ShopItem> purchasedItems = new List<ShopItem>();

    public void AddItem(ShopItem item)
    {
        purchasedItems.Add(item);
        Debug.Log($"Added {item.name} to inventory.");
    }

    public void RemoveItem(ShopItem item)
    {
        if (purchasedItems.Contains(item))
        {
            purchasedItems.Remove(item);
            Debug.Log($"Removed {item.name} from inventory.");
        }
    }

    public bool PlaceItemOnMap(ShopItem item, Vector3 position)
    {
        if (purchasedItems.Contains(item))
        {
            // Instantiate the item prefab on the map
            // e.g. Instantiate(Resources.Load<GameObject>(item.id), position, Quaternion.identity);

            // Remove from inventory once placed
            RemoveItem(item);
            return true;
        }
        return false;
    }
}
