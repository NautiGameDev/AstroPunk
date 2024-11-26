
/*
NPC Object that is instantiated into the world upon chunk generation.
*/

public class NPCEntity
{
    public enum NPCState {ALIVE, DEAD}
    public NPCState currentState = NPCState.ALIVE;

    public string entityName;
    string npcRace;
    int npcAge;

    //Appearance
    string npcSex;
    string npcSkinTone;
    string npcHairColor;
    string npcHairType;
    string npcEyeColor;
    string npcFacialHair;

    //Base stats
    int npcVitality;
    int npcStrength;
    int npcDexterity;
    int npcAgility;
    int npcCharisma;

    //Dynamic stats
    int npcMaxHealth;
    int npcCurrentHealth;
    int npcMeleeDamage;
    int npcRangeDamage;
    float npcDodgeChance;

    public Dictionary<ItemEntity, float> dropTableDict = new Dictionary<ItemEntity, float>();
    public List<ItemEntity> equipment = new List<ItemEntity>();



    string attackMessage;
    string listenMessage;
    string inspectMessage;
    string gatherMessage;
    string lootMessage;

    string deadAttackMessage;
    string deadListenMessage;
    string deadInspectMessage;
    string deadGatherMessage;
    string deadLootMessage;

    //References
    World gameWorld;


    public NPCEntity(NPCData npcData, string npc, World world)
    {
        Dictionary<string, string> npcInfo = npcData.NPCDict[npc];

        entityName = npcInfo["EntityName"];
        npcRace = npcInfo["EntityRace"];
        
        npcVitality = int.Parse(npcInfo["Vitality"]);
        npcStrength = int.Parse(npcInfo["Strength"]);
        npcDexterity = int.Parse(npcInfo["Dexterity"]);
        npcAgility = int.Parse(npcInfo["Agility"]);
        npcCharisma = int.Parse(npcInfo["Charisma"]);

        gameWorld = world;
        
        string[] dropTableStrings = npcInfo["DropTable"].Split("/");
        PopulateDropTable(dropTableStrings);

        string[] equipmentStrings = npcInfo["Equipment"].Split("/");
        PopulateEquipment(equipmentStrings);

        attackMessage = npcInfo["AttackMessage"];
        listenMessage = npcInfo["ListenMessage"];
        inspectMessage = npcInfo["InspectMessage"];
        gatherMessage = npcInfo["GatherMessage"];
        lootMessage = npcInfo["LootMessage"];

        deadAttackMessage = npcInfo["DeadAttackMessage"];
        deadListenMessage = npcInfo["DeadListenMessage"];
        deadInspectMessage = npcInfo["DeadInspectMessage"];
        deadGatherMessage = npcInfo["DeadGatherMessage"];
        deadLootMessage = npcInfo["DeadLootMessage"];

        UpdateDynamicStats();
    }


#region object instantiation methods

    void PopulateDropTable(string[] dropTableStrings)
    {
        foreach (string itemString in dropTableStrings)
        {
            string[] newStringArray = itemString.Split("%");
            if (newStringArray[0] != "")
            {
                ItemEntity item = gameWorld.CreateItemFromData(newStringArray[0]);
                dropTableDict.Add(item, float.Parse(newStringArray[1]));
            }
        }
    }

    void PopulateEquipment(string[] equipmentStrings)
    {
        if (equipmentStrings[0] != "")
        {
            for (int i = 0; i<equipmentStrings.Length; i++)
            {
                ItemEntity newItem = gameWorld.CreateItemFromData(equipmentStrings[i]);
                equipment.Add(newItem);
            }
        }
    }

    public void UpdateDynamicStats()
    {
        Dictionary<string, float> statScales = Utilities.GetStatScales();

        npcMaxHealth = npcVitality * (int)statScales["Vitality"];
        npcCurrentHealth = npcMaxHealth;
        npcMeleeDamage = npcStrength * (int)statScales["Strength"];
        npcRangeDamage = npcDexterity * (int)statScales["Dexterity"];
        npcDodgeChance = npcDexterity * statScales["Agility"];
    }

#endregion

#region Message Getter methods
    public string GetGatherMessage()
    {

        return currentState == NPCState.DEAD ? deadGatherMessage : gatherMessage;
    }

    public string GetInspectMessage()
    {
        return currentState == NPCState.DEAD ? deadInspectMessage : inspectMessage;
    }

    public string GetListenMessage()
    {
        return currentState == NPCState.DEAD ? deadListenMessage : listenMessage;
    }

    public string GetAttackMessage()
    {
        return currentState == NPCState.DEAD ? deadAttackMessage : attackMessage;
    }

    public string GetLootMessage()
    {
        return currentState == NPCState.DEAD ? deadLootMessage: lootMessage;
    }
    
#endregion

#region Combat Methods

    public bool IsAlive()
    {
        return currentState == NPCState.ALIVE ? true : false;
    }

    public int[] ResolveDamage(int dmg)
    {
        npcCurrentHealth -= dmg;

        if (npcCurrentHealth < 0)
        {
            npcCurrentHealth = 0;
        }

        int[] npcHealthStats = new int[] {npcCurrentHealth, npcMaxHealth};
        return npcHealthStats;
    }

    public int GetBaseDamage()
    {
        foreach (ItemEntity eq in equipment)
        {
            if (eq.range != 0)
            {
                if (eq.range == 1)
                {
                    return npcMeleeDamage;
                }
                else if (eq.range == 2)
                {
                    return npcRangeDamage;
                }
            }
        }

        return npcMeleeDamage;
    }

    public int GetCurrentHealth()
    {
        return npcCurrentHealth;
    }

    public int GetWeaponDice()
    {
        int diceToRoll = 0;

        foreach (ItemEntity item in equipment)
        {
            diceToRoll += item.GetDiceQuantity();
        }

        return diceToRoll;
    }

    public int GetArmorStat()
    {
        int armor = 0;
        foreach (ItemEntity item in equipment)
        {
            armor += item.GetArmorQuantity();
        }

        return armor;
    }

    public List<ItemEntity> GetLoot()
    {
        //Returns list of items to actions handle which is iterated through and each item is added to player inventory
        //Random item is chosen from drop table based on probability, then a list is created with the equiped items + random drop item

        List<ItemEntity> lootedItems = new List<ItemEntity>();

        float randomNumb = Utilities.GetRandomFloat_0to1();
        float highestChance = 1f;
        ItemEntity chosenItem = null;

        foreach (ItemEntity item in dropTableDict.Keys)
        {

            if (dropTableDict[item] > randomNumb && dropTableDict[item] <= highestChance)
            {
                highestChance = dropTableDict[item];
                chosenItem = item;
            }
        }

        if (chosenItem != null)
        {
            lootedItems.Add(chosenItem);
        }


        for (int i = 0; i < equipment.Count; i++)
        {
            lootedItems.Add(equipment[i]);
        }

        return lootedItems;

    }

#endregion

}

