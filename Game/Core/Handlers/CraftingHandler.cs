using System.Globalization;

public class CraftingHandler
{
    TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;

    ActionHandler actionHandler;
    Player player;
    World world;

    public CraftingHandler(ActionHandler AH)
    {
        actionHandler = AH;
        player = AH.Get_PlayerReference();
        world = AH.Get_WorldReference();
    }

    public string Process_Crafting(string target, int amount)
    {
        string t = textInfo.ToTitleCase(target);

        Dictionary<string, string> recipe = player.GetRecipe(t);

        if (!PlayerHasEquipment(recipe))
        {
            return "You need a " + recipe["Equipment Required"] + " to craft a " + target + ".";
        }

        if (recipe != null)
        {
            if (PlayerHasIngredients(recipe, amount))
            {
                return Craft_Target(t, recipe, amount);
            }
        }
        else
        {
            return "You don't have the ingredients required to craft " + target + ".";
        }

        return "";
    }

    bool PlayerHasEquipment(Dictionary<string, string> recipe)
    {
        string equipmentRequired = recipe["Equipment Required"].ToLower();

        if (equipmentRequired == "none") return true;

        ChunkData currentChunk = world.GetChunkAtWorldCoords();
        EnvironmentEntity environmentObj = actionHandler.GetEnvironmentObj(equipmentRequired, currentChunk);

        if (environmentObj == null)
        {
            return false;
        }

        return true;
    }

    bool PlayerHasIngredients(Dictionary<string, string> recipe, int amount)
    {
        string[] materialsRequired = recipe["Materials Required"].Split("/");

        foreach (string item in materialsRequired)
        {
            string[] material = item.Split("%");

            int quantityOnHand = player.GetItemQuantityInInventory(material[0].ToLower());                   

            if (quantityOnHand < (int.Parse(material[1]) * amount))
            {
                return false;
            }
        }

        return true;
    }

    string Craft_Target(string target, Dictionary<string, string> recipe, int amount)
    {
        string[] materialsRequired = recipe["Materials Required"].Split("/");
        string messageToReturn = "";

        for (int i=0; i<amount; i++)
        {
            EnvironmentEntity newEnvironment = world.CreateEnvironmentFromData(target);

            if (newEnvironment != null)
            {
                world.AddEnvironmentToChunk(newEnvironment);
                RemoveMaterialsFromInventory(materialsRequired);

                messageToReturn = "You craft a " + target + " and place it in the world.";
            }
        }

        if (messageToReturn != "")
        {
            return messageToReturn;
        }

        for (int i=0; i<amount; i++)
        {

            ItemEntity newItem = world.CreateItemFromData(target);

            if (newItem != null)
            {
                player.AddItemToInventory(newItem);
                RemoveMaterialsFromInventory(materialsRequired);
                messageToReturn = "You craft a " + target + " and place it in your pack.";
            }
        }

        return messageToReturn;
    }

    void RemoveMaterialsFromInventory(string[] materialsRequired)
    {
        foreach (string item in materialsRequired)
            {
                string[] material = item.Split("%");
                player.RemoveItemFromInventory(material[0].ToLower(), int.Parse(material[1]));
            }
    }

    public string SmeltItem(string target, int amount, string subtarget)
    {
        if (subtarget == "")
        {
            return "You need to specify where you want to smelt the " + target;
        }

        if (!subtarget.Contains("refiner"))
        {
            return "You can only refine items in a refiner.";
        }

        ChunkData currentChunk = world.GetChunkAtWorldCoords();
        
        ItemEntity item = actionHandler.GetItemInInventory(target);

        if (item == null)
        {
            return "You don't have any " + target + " in your inventory.";
        }

        if (!item.CanBeSmelted())
        {
            return "You can't refine " + target + " further.";
        }

        int itemOH = player.GetItemQuantityInInventory(target);

        if (itemOH < amount)
        {
            return "You don't have enough " + target + " to do that.";
        }

        EnvironmentEntity envObj = actionHandler.GetEnvironmentObj(subtarget, currentChunk);

        if (envObj == null)
        {
            return subtarget + " doesn't exist in the area.";
        }

        int numbRefined = 0;

        for (int i = 0; i<amount; i++)
        {
            if (envObj.CheckForFuel())
            {
                envObj.ReduceCharge();
                ItemEntity ingot = item.SmeltItem(world);
                player.RemoveItemFromInventory(target, 1);
                player.AddItemToInventory(ingot);
                numbRefined++;
            }
            else
            {
                if (numbRefined == 0)
                {
                    return "You don't have any usable fuel in the " + subtarget + ". Add carbon to refine your ores.";
                }
                else
                {
                    return "You refined " + numbRefined + " " + target + " into ingots, but ran out of fuel in the " + subtarget + ". Add more carbon to continue refining. The ingots have been placed in your pack.";
                }
            }
        }

        return "You refined " + numbRefined + " " + target + " into ingots. The ingots have been placed in your pack.";
    }
}