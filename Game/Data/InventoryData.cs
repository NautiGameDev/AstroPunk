
public class InventoryData
{
    Dictionary<ItemEntity, int> inventoryDict = new Dictionary<ItemEntity, int>();

    float currentWeight;

    public float GetCurrentInventoryWeight()
    {
        return currentWeight;
    }

    public void PlaceItemInInventory(ItemEntity item)
    {
        foreach (ItemEntity heldItem in inventoryDict.Keys)
        {
            if (heldItem.entityName == item.entityName)
            {
                inventoryDict[heldItem] += 1;
                currentWeight += item.weight_kg;
                return;
            }
        }

        inventoryDict.Add(item, 1);
        
    }

    public void RemoveItemInInventory(string target)
    {
        foreach (ItemEntity item in inventoryDict.Keys)
        {
            if (item.entityName.ToLower() == target)
            {
                inventoryDict.Remove(item);
                break;
            }
        }
    }

    public void RemoveItemInInventory(string target, int amount)
    {
        foreach (ItemEntity item in inventoryDict.Keys)
        {
            if (item.entityName.ToLower() == target)
            {
                inventoryDict[item] -= amount;
                currentWeight -= item.weight_kg * amount;

                if (inventoryDict[item] <= 0)
                {
                    inventoryDict.Remove(item);
                }

                break;
            }
        }
    }

    public ItemEntity GetItemInInventory(string target)
    {
        foreach (ItemEntity item in inventoryDict.Keys)
        {
            if (item.entityName.ToLower() == target)
            {
                return item;
            }
        }

        return null;
    }

    public int GetItemQuantity(string target)
    {
        foreach (ItemEntity item in inventoryDict.Keys)
        {
            if (item.entityName.ToLower() == target)
            {
                return inventoryDict[item];
            }
        }

        return 0;
    }

    public string GetInventory()
    {
        if (inventoryDict.Count == 0)
            {
                return "You pack is empty.";
            }

        string inventoryString = "Current Inventory:";

        foreach (ItemEntity item in inventoryDict.Keys)
        {
            inventoryString += "\n" + item.entityName + " x" + inventoryDict[item];
        }

        return inventoryString;
    }
}
