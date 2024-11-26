
public class ActionHandler
{
    

    public World world;
    public Player player;

    string previousBiomeDescribed;

    Dictionary<string, HashSet<string>> synonymDictionary = new Dictionary<string, HashSet<string>>()
    {
        {"move", new HashSet<string> {"move", "go", "travel"}},
        {"north", new HashSet<string> {"north", "n"}},
        {"east", new HashSet<string> {"east", "e"}},
        {"west", new HashSet<string> {"west", "w"}},
        {"south", new HashSet<string> {"south", "s"}},
        {"get", new HashSet<string> {"harvest", "gather", "get", "g", "take", "grab"}},
        {"look", new HashSet<string> {"look", "l", "inspect", "examine", "view"}},
        {"listen", new HashSet<string> {"listen", "hear"}},
        {"attack", new HashSet<string> {"attack", "a", "kill", "murder", "fight", "destroy"}},
        {"loot", new HashSet<string> {"loot"}},
        {"eq", new HashSet<string> {"equipment", "eq", "equip", "wear", "e", "wield"}},
        {"inv", new HashSet<string> {"inventory", "i", "inv", "pack"}},
        {"drop", new HashSet<string> {"drop", "throw", "d"}},
        {"unequip", new HashSet<string> {"unequip", "remove"}},
        {"use", new HashSet<string> {"use", "utilize"}},
        {"consume", new HashSet<string> {"consume", "drink", "eat", "c"}},
        {"status", new HashSet<string> {"diagnostics", "status"}},
        {"craft", new HashSet<string> {"craft", "make", "create"}},
        {"refine", new HashSet<string> {"refine", "smelt"}},
        {"fuel", new HashSet<string> {"fuel", "refuel", "fill", "refill"}},
        {"/emote", new HashSet<string> {"/me", "/emote"}},
        {"/help", new HashSet<string> {"/help", "help", "guide", "tutorial", "how"}}
    };

    public ActionHandler(World w, Player p)
    {
        world = w;
        player = p;
    }

#region Parsing methods
    public string GetInputAction(string[] parsedInput)
    {
        string newMessage = "";

        string action = Get_Action(parsedInput);
        string target = Get_Target(parsedInput);

        switch (action)
        {
            case "move":
                newMessage += new MovementHandler(this).ProcessMovement(target);
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
                newMessage += new GatherHandler(this).Process_Gathering(target);
                break;

            case "look":
                newMessage += new InspectHandler(this).Process_Inspect(target);
                break;

            case "listen":
                newMessage += new ListenHandler(this).Process_Listen(target);
                break;

            case "attack":
                newMessage += new CombatHandler(this).Process_Combat(target);
                break;

            case "loot":
                if (parsedInput.Length == 2) 
                {
                    target += " corpse";
                }
                newMessage += new InventoryHandler(this).Process_Loot(target);
                break;


            case "inv":
                newMessage += new InventoryHandler(this).Process_GetInventory();
                break;
            
            case "eq":
                if (parsedInput.Length == 1)
                    newMessage += new InventoryHandler(this).Process_GetEquipment();
                else if (parsedInput.Length > 1)
                    newMessage += new InventoryHandler(this).Process_Equip(target);
                break;

            case "drop":
                newMessage += new InventoryHandler(this).Process_Drop(target);
                break;

            case "unequip":
                newMessage += new InventoryHandler(this).Process_Unequip(target);
                break;

            case "status":
                newMessage += new PlayerHandler(this).Process_Diagnostics();
                break;
            
            case "consume":
                newMessage += new InventoryHandler(this).Process_Consume(target);
                break;

            case "craft":
                newMessage += new CraftingHandler(this).Process_Crafting(target);
                break;

            case "fuel":
                if (parsedInput.Length == 2) target += " 1";
                newMessage += new CraftingHandler(this).Process_Fuel(target);
                break;

            case "/emote":
                newMessage += new PlayerHandler(this).Process_Emote(parsedInput);
                break;
        }

        newMessage += player.ResolveTurn();
        return newMessage + "\n";
    }

    string Get_Action(string[] parsedInput)
    {
        string action = "";

        foreach (var entry in synonymDictionary)
        {
            if (entry.Value.Contains(parsedInput[0]))
            {
                action = entry.Key;
                break;
            }
        }

        return action;
    }
    string Get_Target(string[] parsedInput)
    {
        string target = "";

        if (parsedInput.Length >= 3)
        {
            target = parsedInput[1] + " " + parsedInput[2];
        }

        else if (parsedInput.Length == 2)
        {
            target = parsedInput[1];
        }

        return target;
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
