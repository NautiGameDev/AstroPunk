using System;

public class ChunkData
{
    public string biomeName;
    public int[] chunkPosition;
    float altitude;
    float humidity;
    float temperature;
    
    public string[] descriptions;
    public List<NPCEntity> storedNPCs = new List<NPCEntity>();
    public List<ItemEntity> storedItems = new List<ItemEntity>();
    public List<EnvironmentEntity> storedEnvironmentObj = new List<EnvironmentEntity>();

    public ChunkData(int[] worldPosition, BiomeEntity currentBiome)
    {
        chunkPosition = worldPosition;
        biomeName = currentBiome.biomeName;
        altitude = currentBiome.altitude;
        temperature = currentBiome.temperature;
        humidity = currentBiome.humidty;
        descriptions = currentBiome.descriptions;
    }

#region Add/Remove Items in World
    public void AddNPCToList(NPCEntity npc)
    {
        storedNPCs.Add(npc);
    }

    public void AddItemToList(ItemEntity item)
    {
        storedItems.Add(item);
    }

    public void AddEnvironmentToList(EnvironmentEntity environment)
    {
        storedEnvironmentObj.Add(environment);
    }

    public void RemoveItemFromList(ItemEntity targetItem)
    {
        foreach (ItemEntity item in storedItems)
        {
            if (item.entityName == targetItem.entityName)
            {
                storedItems.Remove(item);
                break;
            }
        }
    }

    public void RemoveNPCFromList(NPCEntity targetNPC)
    {
        foreach (NPCEntity npc in storedNPCs)
        {
            if (npc.entityName == targetNPC.entityName)
            {
                storedNPCs.Remove(npc);
                break;
            }
        }
    }

    public void RemoveEnvironmentFromList(EnvironmentEntity targetEnvironment)
    {
        foreach (EnvironmentEntity environmentObj in storedEnvironmentObj)
        {
            if (environmentObj.entityName == targetEnvironment.entityName)
            {
                storedEnvironmentObj.Remove(environmentObj);
                break;
            }
        }
    }
#endregion

    public string GetRandomDescription()
    {
        Random random = new Random();
        int randomChoice = Utilities.GetRandomInt(0, descriptions.Length - 1);
        return descriptions[randomChoice];
    }

#region Get Entities In Chunk Methods
    public NPCEntity GetTargetNPC(string target)
    {
        foreach (NPCEntity npc in storedNPCs)
        {
            if (npc.entityName.ToLower() == target)
            {
                return npc;
            }
        }

        return null;
    }

    public ItemEntity GetTargetItem(string target)
    {
        foreach (ItemEntity item in storedItems)
        {
            if (item.entityName.ToLower() == target)
            {
                return item;
            }
        }

        return null;
    }

    public EnvironmentEntity GetTargetEnvironmentObj(string target)
    {
        foreach (EnvironmentEntity obj in storedEnvironmentObj)
        {
            if (obj.entityName.ToLower() == target)
            {
                return obj;
            }
        }

        return null;
    }
#endregion

}

