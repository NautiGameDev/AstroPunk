
public class ActionHandler
{
    

    public World world;
    public Player player;
    Parser parser = new Parser();

    string previousBiomeDescribed;

    public ActionHandler(World w, Player p)
    {
        world = w;
        player = p;
    }

#region Parsing methods
    public string GetInputAction(string playerInput)
    {
        string newMessage = "";

        Dictionary<string, string> parsedInput = parser.ParseInput(playerInput);
        
        switch (parsedInput["action"])
        {
            case "move":
                newMessage += new MovementHandler(this).ProcessMovement(parsedInput["target"]);
                break;

            case "north":
                newMessage += new MovementHandler(this).ProcessMovement("n");
                break;

            case "east":
                newMessage += new MovementHandler(this).ProcessMovement("e");
                break;

            case "south":
                newMessage += new MovementHandler(this).ProcessMovement("s");
                break;

            case "west":
                newMessage += new MovementHandler(this).ProcessMovement("w");
                break;
 
            case "get":
                newMessage += new GatherHandler(this).Process_Gathering(parsedInput["target"], parsedInput["amount"], parsedInput["subtarget"]);
                break;

            case "look":
                newMessage += new InspectHandler(this).Process_Inspect(parsedInput["target"]);
                break;

            case "listen":
                newMessage += new ListenHandler(this).Process_Listen(parsedInput["target"]);
                break;

            case "attack":
                newMessage += new CombatHandler(this).Process_Combat(parsedInput["target"]);
                break;

            case "inv":
                newMessage += new InventoryHandler(this).Process_GetInventory();
                break;
            
            case "eq":
                if (parsedInput["target"] == "")
                    newMessage += new InventoryHandler(this).Process_GetEquipment();
                else
                    newMessage += new InventoryHandler(this).Process_Equip(parsedInput["target"]);
                break;

            case "drop":
                newMessage += new InventoryHandler(this).Process_Drop(parsedInput["target"], int.Parse(parsedInput["amount"]));
                break;

            case "unequip":
                newMessage += new InventoryHandler(this).Process_Unequip(parsedInput["target"]);
                break;

            case "status":
                newMessage += new PlayerHandler(this).Process_Diagnostics();
                break;
            
            case "consume":
                newMessage += new InventoryHandler(this).Process_Consume(parsedInput["target"], int.Parse(parsedInput["amount"]));
                break;

            case "craft":
                newMessage += new CraftingHandler(this).Process_Crafting(parsedInput["target"], int.Parse(parsedInput["amount"]));
                break;

            case "add":
                newMessage += new InventoryHandler(this).AddItemToEnvironment(parsedInput["target"], int.Parse(parsedInput["amount"]), parsedInput["subtarget"]);
                break;

            case "refine":
                newMessage += new CraftingHandler(this).SmeltItem(parsedInput["target"], int.Parse(parsedInput["amount"]), parsedInput["subtarget"]);
                break;

            case "/emote":
                newMessage += new PlayerHandler(this).Process_Emote(parsedInput["target"]);
                break;
        }

        newMessage += player.ResolveTurn();
        return newMessage + "\n";
    }
#endregion



#region Get Entities
    public EnvironmentEntity GetEnvironmentObj(string target, ChunkData currentChunk)
    {
        EnvironmentEntity environmentObj = currentChunk.GetTargetEnvironmentObj(target);

        if (environmentObj != null)
        {
            return environmentObj;
        }

        return null;
    }

    public ItemEntity GetItemInWorld(string target, ChunkData currentChunk)
    {
        ItemEntity item = currentChunk.GetTargetItem(target);

        if (item != null)
        {
            return item;
        }

        return null;
    }

    public ItemEntity GetItemInInventory(string target)
    {
        ItemEntity item = player.GetItemFromInventory(target);
        return item;
    }

    public ItemEntity GetItemInEquipment(string target)
    {
        ItemEntity item = player.GetItemEquipped(target);
        return item;
    }

    public NPCEntity GetNPC(string target, ChunkData currentChunk)
    {
        NPCEntity NPC = currentChunk.GetTargetNPC(target);

        if (NPC != null)
            return NPC;

        return null;
    }

    public Player Get_PlayerReference()
    {
        return player;
    }

    public World Get_WorldReference()
    {
        return world;
    }

#endregion


}
