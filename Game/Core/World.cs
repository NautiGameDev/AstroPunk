
using System.CodeDom.Compiler;

public class World
{
    public Player player;

    //Chunk handling
    int[] worldCoordinates = new int[2] {0,0};
    WorldData worldData = new WorldData();
    BiomeEntity currentBiome;

    //Data
    BiomeData biomeData = new BiomeData();
    ItemData itemData = new ItemData();
    NPCData npcData = new NPCData();
    EnvironmentData environmentData = new EnvironmentData();

    

    public World(Player p)
    {
        player = p;
        UpdatePosition(worldCoordinates);
        player.SetGameWorld(this);
    }

    
    void GenerateNewChunk()
    {
        //Needs improved when implementing world generation
        currentBiome = new BiomeEntity(biomeData, "Test Biome");

        ChunkData currentChunk = new ChunkData(worldCoordinates, currentBiome);        

        //Populate NPCs in Chunk Data
        for (int i=0; i < currentBiome.numbNPCObj; i++)
        {
            float randFloat = Utilities.GetRandomFloat_0to1();
            float highestChance = 1f;
            string chosenNPC = "";

            foreach (string npc in currentBiome.npcEntities)
            {
                string[] npcArray = npc.Split("%");

                if (float.Parse(npcArray[1]) > randFloat && float.Parse(npcArray[1]) <= highestChance)
                {
                    highestChance = float.Parse(npcArray[1]);
                    chosenNPC = npcArray[0];
                }
            }

            if (chosenNPC != "")
            {
                NPCEntity newNPC = new NPCEntity(npcData, chosenNPC, this);
                currentChunk.AddNPCToList(newNPC);
            }
        }

        //Populate Environment Objects in chunk data
        for (int i=0; i < currentBiome.numbEnvironmentObj; i++)
        {
            float randFloat = Utilities.GetRandomFloat_0to1();
            float highestChance = 1f;
            string chosenObj = "";

            

            foreach (string obj in currentBiome.environmentEntities)
            {
                string[] objArray = obj.Split("%");

                if (float.Parse(objArray[1]) > randFloat && float.Parse(objArray[1]) <= highestChance)
                {
                    highestChance = float.Parse(objArray[1]);
                    chosenObj = objArray[0];
                }
            }

            if (chosenObj != "")
            {
                EnvironmentEntity newEnvironmentObj = new EnvironmentEntity(environmentData, chosenObj, this);
                currentChunk.AddEnvironmentToList(newEnvironmentObj);
            }
        }

        //Populate items in chunk data
        for (int i=0; i < currentBiome.numbItemObj; i++)
        {
            float randFloat = Utilities.GetRandomFloat_0to1();
            float highestChance = 1f;
            string chosenItem = "";

            foreach (string item in currentBiome.itemEntities)
            {
                string[] itemArray = item.Split("%");

                if (float.Parse(itemArray[1]) > randFloat && float.Parse(itemArray[1]) <= highestChance)
                {
                    highestChance = float.Parse(itemArray[1]);
                    chosenItem = itemArray[0];
                }
            }

            if (chosenItem != "")
            {
                ItemEntity newItem = new ItemEntity(itemData, chosenItem);
                currentChunk.AddItemToList(newItem);
            }
        }

        worldData.AddChunkToDictionary(worldCoordinates, currentChunk);
    }

#region GUI Methods

    public string[] GetEntitiesInChunk(ChunkData chunk)
    {
        string[] entityList = new string[3];
        int npcIndex = 0;
        int maxNPCIndex = chunk.storedNPCs.Count;

        foreach (NPCEntity npc in chunk.storedNPCs)
        {
            npcIndex++;
            entityList[0] += npc.entityName;
            if (npcIndex != maxNPCIndex)
            {
                entityList[0] += ", ";
            }
        }

        entityList[0] += "\n";
        int envIndex = 0;
        int maxEnvIndex = chunk.storedEnvironmentObj.Count;

        foreach (EnvironmentEntity environmentObj in chunk.storedEnvironmentObj)
        {
            envIndex++;
            entityList[1] += environmentObj.entityName;
            if (envIndex != maxEnvIndex)
            {
                entityList[1] += ", ";
            }
        }

        entityList[1] += "\n";
        int itemIndex = 0;
        int maxItemIndex = chunk.storedItems.Count;

        foreach (ItemEntity item in chunk.storedItems)
        {
            itemIndex++;
            entityList[2] += item.entityName;
            if (itemIndex != maxItemIndex)
            {
                entityList[2] += ", ";
            }
        }

        entityList[2] += "\n";

        return entityList;
    }
#endregion

#region Setter and Getter Methods
    public int[] GetCoordinates()
    {
        return worldCoordinates;
    }

    public string GetBiomeInformation()
    {
        return currentBiome.biomeName;
    }

    public ChunkData GetChunkAtWorldCoords()
    {
        return worldData.GetChunkAtPosition(worldCoordinates);
    }

    public void UpdatePosition(int[] coordMovement)
    {

        //Update to generate chunks n,w,s,e of player coordinates
        worldCoordinates[0] += coordMovement[0];
        worldCoordinates[1] += coordMovement[1];

        try
        {
            ChunkData chunk = GetChunkAtWorldCoords();
        }
        catch
        {
            GenerateNewChunk();
        }
    }

    public void SetPosition(int[] newPos)
    {
        worldCoordinates = newPos;
        ChunkData chunk = GetChunkAtWorldCoords();
    }

    public ItemEntity CreateItemFromData(string item)
    {
        ItemEntity newItem = new ItemEntity(itemData, item);
        return newItem;
    }

    public EnvironmentEntity CreateEnvironmentFromData(string obj)
    {
        EnvironmentEntity newObj = new EnvironmentEntity(environmentData, obj, this);
        return newObj;
    }

#endregion

#region Add/Remove Entities from chunk
    public void AddItemToCurrentChunk(ItemEntity item)
    {
        ChunkData currentChunk = GetChunkAtWorldCoords();
        currentChunk.AddItemToList(item);
    }

    public void RemoveItemFromChunk(ItemEntity item)
    {
        ChunkData currentChunk = GetChunkAtWorldCoords();
        currentChunk.RemoveItemFromList(item);
    }

    public void AddNPCToChunk(NPCEntity npc)
    {
        ChunkData currentChunk = GetChunkAtWorldCoords();
        currentChunk.AddNPCToList(npc);
    }

    public void RemoveNPCFromChunk(NPCEntity npc)
    {
        ChunkData currentChunk = GetChunkAtWorldCoords();
        currentChunk.RemoveNPCFromList(npc);
        
    }

    public void AddEnvironmentToChunk(EnvironmentEntity environmentObj)
    {
        ChunkData currentChunk = GetChunkAtWorldCoords();
        currentChunk.AddEnvironmentToList(environmentObj);
        
    }

    public void RemoveEnvironmentFromChunk(EnvironmentEntity environmentObj)
    {
        ChunkData currentChunk = GetChunkAtWorldCoords();
        currentChunk.RemoveEnvironmentFromList(environmentObj);
        
    }

#endregion

}
