public class Player
{

    World world;

    CraftingData craftingData  = new CraftingData();

    InventoryData playerInventory = new InventoryData();

    EquipmentData playerEquipment = new EquipmentData();

    string playerName;
    string playerAge;

    //Change to enums for character creator
    string playerSex;
    string playerSkinTone;
    string playerHairColor;
    string playerHairType;
    string playerEyeColor;
    string playerFacialHair;


    //Base stats
    int playerVitality;
    int playerStrength;
    int playerDexterity;
    int playerAgility;
    int playerCharisma;
    int playerIntelligence;

    //Dynamic Stats
    int playerHealth;
    int playerMaxHealth;
    int playerMaxOxygen = 100;
    int playerOxygen = 100;
    int playerMeleeDamage;
    int playerRangeDamage;
    float playerDodgeChance;
    float maxCarryWeight;
    
    //Leveling
    int playerLevel = 1;
    int playerXP = 0;
    int playerNextLevel = 500;
    
    public Player(PlayerData playerData)
    {
        //Import details
        Dictionary<string, string> playerDetails = playerData.GetDetails();


        playerName = playerDetails["Name"];
        playerAge = playerDetails["Age"];
        playerSex = playerDetails["Sex"];
        playerSkinTone = playerDetails["SkinTone"];
        playerHairColor = playerDetails["HairColor"];
        playerHairType = playerDetails["HairType"];
        playerEyeColor = playerDetails["EyeColor"];
        playerFacialHair = playerDetails["FacialHair"];

        //Import stats
        Dictionary<string, int> playerStats = playerData.GetStats();

        playerVitality = playerStats["Vitality"];
        playerStrength = playerStats["Strength"];
        playerDexterity = playerStats["Dexterity"];
        playerAgility = playerStats["Agility"];
        playerCharisma = playerStats["Charisma"];
        playerIntelligence = playerStats["Intelligence"];


        CalculateDynamicStats();
    }

    void CalculateDynamicStats()
    {
        Dictionary<string, float> statScales = Utilities.GetStatScales();

        playerMaxHealth = playerVitality * (int)statScales["Vitality"];
        playerHealth = playerMaxHealth;
        playerMeleeDamage = playerStrength * (int)statScales["Strength"];
        playerRangeDamage = playerDexterity * (int)statScales["Dexterity"];
        playerDodgeChance = playerAgility * statScales["Agility"];
        maxCarryWeight = playerStrength * statScales["CarryWeight"];
    }

    public string ResolveTurn()
    {
        string messageToReturn = ""; 
        playerOxygen -= 1;

        if (playerOxygen < 0)
        {
            playerOxygen = 0;
            messageToReturn += "\nWARNING: Oxygen level critical.";
            playerHealth -= (int)(playerMaxHealth * 0.1f);
        }
        else if (playerOxygen < 10)
        {
            messageToReturn += "\nWARNING: Oxygen level at 10%";
        }

        return messageToReturn;
    }

    public int AddOxygen(int amount)
    {
        int o2Needed = (playerMaxOxygen - playerOxygen)/10;

        if (o2Needed <= amount)
        {
            playerOxygen += o2Needed * 10;
            return o2Needed;
        }
        else
        {
            playerOxygen += amount * 10;
            return amount;
        }
    }



#region Inventory Methods

    public string AddItemToInventory(ItemEntity item)
    {
        string stringToReturn = "The " + item.entityName + " has been placed in your pack.";
        playerInventory.PlaceItemInInventory(item);
        return stringToReturn;
    }

    public string AddItemToInventory(ItemEntity item, int amount)
    {
        string stringToReturn = "The " + item.entityName + " has been placed in your pack.";
        playerInventory.PlaceItemInInventory(item);
        return stringToReturn;
    }

    public void RemoveItemFromInventory(string target)
    {
        playerInventory.RemoveItemInInventory(target);
    }

    public void RemoveItemFromInventory(string target, int amount)
    {
        playerInventory.RemoveItemInInventory(target, amount);
    }

    public ItemEntity GetItemFromInventory(string target)
    {
        ItemEntity item = playerInventory.GetItemInInventory(target);
        return item;
    }

    public int GetItemQuantityInInventory(string target)
    {
        int quantity = playerInventory.GetItemQuantity(target);
        return quantity;
    }

    public string GetInventory()
    {
        return playerInventory.GetInventory();
    }
#endregion

#region Equipment Methods

    public string EquipItem(ItemEntity item)
    {
        return playerEquipment.EquipItemToSlot(item);
    }

    public ItemEntity UnequipItem(string target)
    {
        ItemEntity item = playerEquipment.UnequipItemFromSlot(target);
        return item;

    }

    public ItemEntity GetItemEquipped(string target)
    {
        ItemEntity item = playerEquipment.GetItemEquiped(target);
        return item;
    }

    public string GetEquippedItems()
    {
        string equippedItems = playerEquipment.GetEquipedItems();
        return equippedItems;
    }

    public ItemEntity GetEquippedItemInSlot(string slot)
    {
        ItemEntity item = playerEquipment.GetItemInSlot(slot);
        return item;
    }

#endregion

#region set/get methods

    public void SetGameWorld(World gameWorld)
    {
        world = gameWorld;
    }

    public string GetPlayerName()
    {
        return playerName;
    }

    public int GetPlayerLevel()
    {
        return playerLevel;
    }
    
    public int[] GetPlayerXP()
    {
        int[] playerXPData = new int[2] {playerXP, playerNextLevel};
        return playerXPData;
    }

    public int[] GetOxygen()
    {
        int[] oxygenStats = new int[2];
        oxygenStats[0] = playerOxygen;
        oxygenStats[1] = playerMaxOxygen;
        return oxygenStats;
    }

    public int[] GetPlayerHealth()
    {
        int[] healthStats = new int[2];
        healthStats[0] = playerHealth;
        healthStats[1] = playerMaxHealth;
        return healthStats;
    }

#endregion

#region Combat Methods

    public int[] ResolveDamage(int damage)
    {
        playerHealth -= damage;

        return GetPlayerHealth();
    }

    public void RespawnPlayer()
    {
        int[] respawnPos = {0,0};
        world.SetPosition(respawnPos);
        playerHealth = playerMaxHealth;
    }

    public int GetMeleeDamage()
    {
        return playerMeleeDamage;
    }

    public int GetRangeDamage()
    {
        return playerRangeDamage;
    }

    public int GetArmorStat()
    {
        return playerEquipment.GetArmorStat();
    }

#endregion 

#region Crafting

    public Dictionary<string, string> GetRecipe(string target)
    {
        Dictionary<string, string> recipe = craftingData.craftingRecipes[target];
        return recipe;
    }
#endregion

}
