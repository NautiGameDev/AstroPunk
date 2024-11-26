public class EquipmentData
{

    Dictionary<string, ItemEntity> equippedItems = new Dictionary<string, ItemEntity> 
    { 
        {"HeadSlot", null},
        {"ChestSlot", null},
        {"ShouldersSlot", null},
        {"HandSlot", null},
        {"WaistSlot", null},
        {"LegsSlot", null},
        {"FeetSlot", null},
        {"PrimaryWeaponSlot", null},
        {"SecondaryWeaponSlot", null},
    };

    float currentWeight;
    
    Dictionary<string, int> equipmentModifiers = new Dictionary<string, int>() 
    {
        {"Armor", 0},
        {"Strength", 0},
        {"Dexterity", 0},
        {"Vitality", 0},
        {"Intelligence", 0},
        {"Charisma", 0},
        {"Agility", 0}
    };

    public ItemEntity GetItemInSlot(string slot)
    {
        return equippedItems[slot];
    }

    public string EquipItemToSlot(ItemEntity item) //Update to return bool to prevent bugs on equip item error
    {
        switch(item.entitySlot)
        {
            case ItemEntity.equipableSlot.HEAD:
                if (equippedItems["HeadSlot"] == null)
                {
                    equippedItems["HeadSlot"] = item;
                    UpdateEquipmentModifiers(item, "add");
                    return "You equip the " + item.entityName + "to your head slot.";
                }
                else
                {
                    return "You already have an item equipped to your head slot";
                }
                
            case ItemEntity.equipableSlot.SHOULDERS:
                if (equippedItems["ShouldersSlot"] == null)
                {
                    equippedItems["ShouldersSlot"] = item;
                    UpdateEquipmentModifiers(item, "add");
                    return "You equip the " + item.entityName + " to your shoulders slot.";
                }
                else
                {
                    return "You already have an item equipped to your shoulders slot.";
                }
            
            case ItemEntity.equipableSlot.CHEST:
                if (equippedItems["ChestSlot"] == null)
                {
                    equippedItems["ChestSlot"] = item;
                    UpdateEquipmentModifiers(item, "add");
                    return "You equip the " + item.entityName + " to your chest slot.";
                }
                else
                {
                    return "You already have an item equipped to your chest slot.";
                }
                
                
            case ItemEntity.equipableSlot.WAIST:
                if (equippedItems["WaistSlot"] == null)
                {
                    equippedItems["WaistSlot"] = item;
                    UpdateEquipmentModifiers(item, "add");
                    return "You equip the " + item.entityName + " to your waist slot.";
                }
                else
                {
                    return "You already have an item equipped to your waist slot.";
                }
                
            
            case ItemEntity.equipableSlot.LEGS:
                if (equippedItems["LegsSlot"] == null)
                {
                    equippedItems["LegsSlot"] = item;
                    UpdateEquipmentModifiers(item, "add");
                    return "You equip the " + item.entityName + " to your legs slot.";
                }
                else
                {
                    return "You already have an item equipped to your legs slot.";
                }

            case ItemEntity.equipableSlot.FEET:
                if (equippedItems["FeetSlot"] == null)
                {
                    equippedItems["FeetSlot"] = item;
                    UpdateEquipmentModifiers(item, "add");
                    return "You equip the " + item.entityName + " to your feet slot.";
                }
                else
                {
                    return "You already have an item equipped to your feet slot.";
                }

            case ItemEntity.equipableSlot.HANDS:
                
                if (equippedItems["HandSlot"] == null)
                {
                    equippedItems["HandSlot"] = item;
                    UpdateEquipmentModifiers(item, "add");
                    return "You equip the " + item.entityName + " to your hands slot.";
                }
                else
                {
                    return "You already have an item equipped to your hands slot.";
                }
                

            case ItemEntity.equipableSlot.PRIMARYWEAPON:
                if (equippedItems["PrimaryWeaponSlot"]==null)
                {
                    equippedItems["PrimaryWeaponSlot"] = item;
                    UpdateEquipmentModifiers(item, "add");
                    return "You equip the " + item.entityName + " to your primary weapon slot.";
                }
                else
                {
                    return "You already have an item equipped to your primary weapon slot.";
                }

            case ItemEntity.equipableSlot.SECONDARYWEAPON:
                if (equippedItems["SecondaryWeaponSlot"] == null)
                {
                    equippedItems["SecondaryWeaponSlot"] = item;
                    UpdateEquipmentModifiers(item, "add");
                    return "You equip the " + item.entityName + " to your secondary weapon slot.";
                }
                else
                {
                    return "You already have an item equipped to your secondary weapon slot.";
                }

            default:
                
                break;
        }

        return "Could not equip " + item.entityName; 
    }

    public ItemEntity UnequipItemFromSlot(string target)
    {
        foreach (string slot in equippedItems.Keys)
        {
            if (equippedItems[slot].entityName.ToLower() == target)
            {
                ItemEntity item = equippedItems[slot];
                equippedItems[slot] = null;
                UpdateEquipmentModifiers(item, "remove");
                return item;
            }
        }
        
        return null;
    }

    public ItemEntity GetItemEquiped(string target)
    {
        foreach (string slot in equippedItems.Keys)
        {
            if (equippedItems[slot].entityName.ToLower() == target)
            {
                ItemEntity item = equippedItems[slot];
                return item;
            }
        }
        
        return null;
    }

    public void UpdateEquipmentModifiers(ItemEntity item, string command)
    {
        if (command == "add")
        {
            foreach (string modifier in item.itemModifiers.Keys)
            {
                equipmentModifiers[modifier] += item.itemModifiers[modifier];
                currentWeight += item.weight_kg;
            }
        }

        if (command == "remove")
        {
            foreach (string modifier in item.itemModifiers.Keys)
            {
                equipmentModifiers[modifier] -= item.itemModifiers[modifier];
                currentWeight -= item.weight_kg;
            }
        }
    }

    public float GetEquipmentWeight()
    {
        return currentWeight;
    }

    public int GetArmorStat()
    {
        return equipmentModifiers["Armor"];
    }

    public string GetEquipedItems()
    {
        string messageToReturn = "Equipped Items:";

        foreach (string slot in equippedItems.Keys)
        {
            string[] cleanedString = slot.Split("Slot");

            if (equippedItems[slot] != null)
            {
                messageToReturn += "\n" + cleanedString[0] + ": " + equippedItems[slot].entityName;
            }
            else
            {
                messageToReturn += "\n" + cleanedString[0] + ": [empty]";
            }
        }
        
        return messageToReturn;
    }
}
