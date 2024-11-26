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

    public string Process_Crafting(string target)
    {
        string t = textInfo.ToTitleCase(target);

        Dictionary<string, string> recipe = player.GetRecipe(t);

        if (!PlayerHasEquipment(recipe))
        {
            return "You need a " + recipe["Equipment Required"] + " to craft a " + target + ".";
        }

        if (recipe != null)
        {
            if (PlayerHasIngredients(recipe))
            {
                return Craft_Target(t, recipe);
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

    bool PlayerHasIngredients(Dictionary<string, string> recipe)
    {
        string[] materialsRequired = recipe["Materials Required"].Split("/");

        foreach (string item in materialsRequired)
        {
            string[] material = item.Split("%");

            int quantityOnHand = player.GetItemQuantityInInventory(material[0].ToLower());                   

            if (quantityOnHand < int.Parse(material[1]))
            {
                return false;
            }
        }

        return true;
    }

    string Craft_Target(string target, Dictionary<string, string> recipe)
    {
        string[] materialsRequired = recipe["Materials Required"].Split("/");

        EnvironmentEntity newEnvironment = world.CreateEnvironmentFromData(target);

        if (newEnvironment != null)
        {
            world.AddEnvironmentToChunk(newEnvironment);
            RemoveMaterialsFromInventory(materialsRequired);

            return "You craft a " + target + " and place it in the world.";
        }

        ItemEntity newItem = world.CreateItemFromData(target);

        if (newItem != null)
        {
            player.AddItemToInventory(newItem);
            RemoveMaterialsFromInventory(materialsRequired);
            return "You craft a " + target + " and place it in your pack.";
        }

        return "";
    }

    void RemoveMaterialsFromInventory(string[] materialsRequired)
    {
        foreach (string item in materialsRequired)
            {
                string[] material = item.Split("%");
                player.RemoveItemFromInventory(material[0].ToLower(), int.Parse(material[1]));
            }
    }

    public string Process_Fuel(string target)
    {
        string[] t = target.Split(" ");

        string messageToReturn = "";

        if (t[0] != "carbon")
        {
            messageToReturn += "You can't add " + t[0] + " to any refiners.";
            return messageToReturn;
        }

        ChunkData currentChunk = world.GetChunkAtWorldCoords();

        string crudeRefiner = "crude refiner";

        EnvironmentEntity crudeRefinerObj = actionHandler.GetEnvironmentObj(crudeRefiner, currentChunk);

        if (crudeRefinerObj != null)
        {
            int quantityOnHand = player.GetItemQuantityInInventory(t[0]);

            if (quantityOnHand >= int.Parse(t[1]))
            {
                ItemEntity item = player.GetItemFromInventory(t[0]);

                for (int i=0; i<int.Parse(t[1]); i++)
                {
                    crudeRefinerObj.AddDropItem(item);
                }

                player.RemoveItemFromInventory(t[0], int.Parse(t[1]));

                messageToReturn += "You add " + t[1] + " " + t[0] + " to the Crude Refiner.";
            }
            else
            {
                messageToReturn += "You don't have enough " + t[0];
            }
        }

        return messageToReturn;
    }
}