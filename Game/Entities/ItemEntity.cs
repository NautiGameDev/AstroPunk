public class ItemEntity
{
       
    public enum entTypes {MATERIAL, CONSUMABLE, EQUIPABLE, KEY}
    public enum equipableSlot {NONE, HEAD, SHOULDERS, CHEST, HANDS, WAIST, LEGS, FEET, PRIMARYWEAPON, SECONDARYWEAPON}

    public string entityName;
    public entTypes entityType;
    public equipableSlot entitySlot;
    public float weight_kg;

    public Dictionary<string, int> itemModifiers = new Dictionary<string, int>() 
    {
        {"Armor", 0},
        {"Strength", 0},
        {"Dexterity", 0},
        {"Vitality", 0},
        {"Intelligence", 0},
        {"Charisma", 0},
        {"Agility", 0},
    };

    public int ATKDiceNumber;
    public int range;
    
    public string attackMessage;
    public string listenMessage;
    public string inspectMessage;
    public string gatherMessage;
    public string lootMessage;

    string refineItem;

    public ItemEntity(ItemData itemData, string item)
    {
        Dictionary<string, string> newItem = itemData.itemDictionary[item];

        entityName = newItem["EntityName"];
        entityType = SetEntityType(newItem["EntityType"]);
        entitySlot = SetEquipableSlot(newItem["EquipableSlot"]);
        weight_kg = float.Parse(newItem["Weight_KG"]);

        Dictionary<string, int> tempItemMods = new Dictionary<string, int>();

        foreach (string modifier in itemModifiers.Keys)
        {
            tempItemMods[modifier] = int.Parse(newItem[modifier]);
        }

        itemModifiers = tempItemMods;

        ATKDiceNumber = int.Parse(newItem["DiceAmount"]);
        range = int.Parse(newItem["Range"]);

        attackMessage = newItem["AttackMessage"];
        listenMessage = newItem["ListenMessage"];
        inspectMessage = newItem["InspectMessage"];
        gatherMessage = newItem["GatherMessage"];
        lootMessage = newItem["LootMessage"];

        refineItem = newItem["RefineItem"];
    }

    private entTypes SetEntityType(string eType)
    {
        if (eType == "Material")
        {
            return entTypes.MATERIAL;
        }
        else if (eType == "Consumable")
        {
            return entTypes.CONSUMABLE;
        }
        else if (eType == "Equipable")
        {
            return entTypes.EQUIPABLE;
        }
        else if (eType == "Key")
        {
            return entTypes.KEY;
        }

        return entTypes.MATERIAL;
    }

    private equipableSlot SetEquipableSlot(string slot)
    {
        if (slot == "Head")
        {
            return equipableSlot.HEAD;
        }
        else if (slot == "Shoulders")
        {
            return equipableSlot.SHOULDERS;
        }
        else if (slot == "Chest")
        {
            return equipableSlot.CHEST;
        }
        else if (slot == "Hands")
        {
            return equipableSlot.HANDS;
        }
        else if (slot == "Waist")
        {
            return equipableSlot.WAIST;
        }
        else if (slot == "Legs")
        {
            return equipableSlot.LEGS;
        }
        else if (slot == "Feet")
        {
            return equipableSlot.FEET;
        }
        else if (slot == "PrimaryWeapon")
        {
            return equipableSlot.PRIMARYWEAPON;
        }
        else if (slot == "SecondaryWeapon")
        {
            return equipableSlot.SECONDARYWEAPON;
        }

        return equipableSlot.NONE;
    }

    public int GetDiceQuantity()
    {
        return ATKDiceNumber;
    }

    public int GetRange()
    {
        return range;
    }

    public int GetArmorQuantity()
    {
        return itemModifiers["Armor"];
    }

    public string GetGatherMessage()
    {
        return gatherMessage;
    }

    public string GetInspectMessage()
    {
        return inspectMessage;
    }

    public string GetListenMessage()
    {
        return listenMessage;
    }

    public string GetAttackMessage()
    {
        return attackMessage;
    }

    public ItemEntity SmeltItem(World world)
    {
        if (refineItem != "")
        {
            return world.CreateItemFromData(refineItem);
        }

        return null;
    }

    public bool CanBeSmelted()
    {
        if (refineItem != "")
        {
            return true;
        }

        return false;
    }
}
