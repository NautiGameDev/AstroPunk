
class Play
{
    Player player;
    World world;
    ActionHandler actionHandler;

    string messageFromAction = "Your journey begins on a strange planet, full of life, resources, and plenty of opportunity for adventure into the unknown.\n";

    public Play(string pName, Dictionary<string, string> pStats)
    {
        PlayerData playerData = new PlayerData(pName, int.Parse(pStats["[VIT]"]), int.Parse(pStats["[STR]"]), int.Parse(pStats["[DEX]"]), int.Parse(pStats["[AGI]"]), int.Parse(pStats["[CHA]"]), int.Parse(pStats["[INT]"]));
        player = new Player(playerData);
        world = new World(player);
        actionHandler = new ActionHandler(world, player);

    }

    public string[] GetMessages()
    {
        string[] messageToReturn = new string[2];
        messageToReturn[0] = DisplayUI();
        messageToReturn[0] += DisplayEntities();
        

        messageToReturn[1] = messageFromAction;
    
        return messageToReturn;
    }

    public string ProcessInput(string playerInput)
    {
        string[] parsedInput = playerInput.Split(" ");
        string messageToReturn = actionHandler.GetInputAction(parsedInput);
        messageFromAction = messageToReturn;

        return "";
    }


    string DisplayUI()
    {
        string pName = player.GetPlayerName();
        int[] pHealth = player.GetPlayerHealth();
        int pLevel = player.GetPlayerLevel();
        int[] pOxygen = player.GetOxygen();
        string UI = "";
        int[] pXP = player.GetPlayerXP();

        int[] worldCoords = world.GetCoordinates();
        string currentBiome = world.GetBiomeInformation();

        UI += "-------------------------------------------------------------------------------------------\n";
        UI += "Name: " + pName + " || Level: "+ pLevel + " || Health: " + pHealth[0] + " / " + pHealth[1] + " || o2: " + pOxygen[0] + " / " + pOxygen[1] + " || XP: " + pXP[0] + " / " + pXP[1] + "\n";
        UI += "-------------------------------------------------------------------------------------------\n";
        UI += "Location: XYZ-890 || X: " + worldCoords[0] + ", Y: " + worldCoords[1] + " || Layer: Planet || Biome: " + currentBiome + "\n";
        UI += "-------------------------------------------------------------------------------------------\n";
            
        return UI;
    }

    string DisplayEntities()
    {
        ChunkData currentChunk = world.GetChunkAtWorldCoords();
        string[] entityLists = world.GetEntitiesInChunk(currentChunk);

        string messageToReturn = "";

        messageToReturn += "## Entities in area ##\n";
        messageToReturn += "NPCs: " + entityLists[0];
        messageToReturn += "Environment: " + entityLists[1];
        messageToReturn += "Items: " + entityLists[2];
        messageToReturn += "-------------------------------------------------------------------------------------------\n";

        return messageToReturn;
    }
}