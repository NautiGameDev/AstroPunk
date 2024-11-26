using System.Runtime;

public class EnvironmentEntity
{
 public enum entTypes {NONE, PLANT, ORE, WATER, FURNITURE}

    public string entityName;
    public entTypes entityType;


    public List<ItemEntity> dropTable = new List<ItemEntity>();
    public Dictionary<ItemEntity, int> inventoryTable = new Dictionary<ItemEntity, int>();

    float charge = 0f;

    public string attackMessage;
    public string listenMessage;
    public string inspectMessage;
    public string gatherMessage;
    public string lootMessage;

    //References
    World gameWorld;

    public EnvironmentEntity(EnvironmentData envData, string envObj, World world)
    {
        Dictionary<string, string> environmentObj = envData.EnvironmentDict[envObj];
        gameWorld = world;

        entityName = environmentObj["EntityName"];
        entityType = SetEntityType(environmentObj["EntityType"]);
        
        string[] dropTableArray = environmentObj["Drop Table"].Split("/");
        PopulateDropTable(dropTableArray);

        string[] inventoryTableArray = environmentObj["Inventory"].Split("/");
        PopulateInventory(inventoryTableArray);


        attackMessage = environmentObj["AttackMessage"];
        listenMessage = environmentObj["ListenMessage"];
        inspectMessage = environmentObj["InspectMessage"];
        gatherMessage = environmentObj["GatherMessage"];
        lootMessage = environmentObj["LootMessage"];

        
    }

    private entTypes SetEntityType(string eType)
    {
        if (eType == "Plant")
        {
            return entTypes.PLANT;
        }
        else if (eType == "Ore")
        {
            return entTypes.ORE;
        }
        else if (eType == "Water")
        {
            return entTypes.WATER;
        }
        else if (eType == "Furniture")
        {
            return entTypes.FURNITURE;
        }

        return entTypes.NONE;
    }

    void PopulateDropTable(string[] dropTableArray)
    {
        if (dropTableArray[0] != "")
        {
            for (int i=0; i < dropTableArray.Length; i++)
            {
                ItemEntity newItem = gameWorld.CreateItemFromData(dropTableArray[i]);
                dropTable.Add(newItem);
            }
        }
    }

    void PopulateInventory(string[] inventoryTableArray)
    {
        foreach (string itemString in inventoryTableArray)
        {
            string[] parsedItemString = itemString.Split("%");
            if (parsedItemString[0] != "")
            {
                ItemEntity newItem = gameWorld.CreateItemFromData(parsedItemString[0]);
                inventoryTable.Add(newItem, int.Parse(parsedItemString[1]));
            }
        }
    }

    public string GetGatherMessage()
    {
        return gatherMessage;
    }

    public string GetInspectMessage()
    {
        
    string returnMessage = inspectMessage;
    
    if (inventoryTable.Count > 0)
    {
        returnMessage += "\n\n" + entityName + " inventory: ";

        foreach (ItemEntity item in inventoryTable.Keys)
        {
            returnMessage += "\n" + item.entityName + " x" + inventoryTable[item];
        }
    }

    return returnMessage;
    }

    public string GetListenMessage()
    {
        return listenMessage;
    }

    public string GetAttackMessage()
    {
        return attackMessage;
    }

    public ItemEntity GetDropItem()
    {
        if (dropTable.Count >= 1)
        {
            return dropTable[0];
        }
        
        return null;
    }

    public List<ItemEntity> GetStoredItems()
    {
        List<ItemEntity> storedItems = new List<ItemEntity>();

        foreach (ItemEntity item in inventoryTable.Keys)
        {
            for (int i=0; i<inventoryTable[item]; i++)
            {
                storedItems.Add(item);
            }
        }

        return storedItems;
    }

    public void AddItemToInventory(ItemEntity item, int amt)
    {
        if (inventoryTable.ContainsKey(item))
        {
            inventoryTable[item] += amt;
        }
        else
        {
            inventoryTable.Add(item, amt);
        }
    }

    public ItemEntity RemoveItemFromInventory(string ent, int amt)
    {
        foreach (ItemEntity item in inventoryTable.Keys)
        {
            if (item.entityName.ToLower() == ent.ToLower())
            {
                if (inventoryTable[item] >= amt)
                {
                    inventoryTable[item] -= amt;

                    if (inventoryTable[item] <= 0)
                    {
                        inventoryTable.Remove(item);
                    }

                    return item;
                }
            }
        }

        return null;
    }
    
    public bool CheckForFuel()
    {
        while (true)
        {
            if (charge > 0.1f)
            {
                return true;
            }
            else
            {
                bool hasCarbon = false;

                foreach (ItemEntity item in inventoryTable.Keys)
                {
                    if (item.entityName == "Carbon")
                    {
                        hasCarbon = true;
                        inventoryTable[item] -= 1;
                        
                        if (inventoryTable[item] == 0)
                        {
                            inventoryTable.Remove(item);
                        }

                        charge += 1f;
                        break;
                    }
                }

                if (hasCarbon == false)
                {
                    return false;
                }
            }
        }
    }

    public void ReduceCharge()
    {
        charge -= 0.1f;
    }
}
