public class InventoryHandler
{
   ActionHandler actionHandler;
   World world;
   Player player;

   public InventoryHandler(ActionHandler AH)
   {
        actionHandler = AH;
        player = AH.Get_PlayerReference();
        world = AH.Get_WorldReference();
   }

   public string Process_Loot(string target)
   {
        ChunkData currentChunk = world.GetChunkAtWorldCoords();

        NPCEntity npc = actionHandler.GetNPC(target, currentChunk);

        if (npc != null)
        {

            string messageToReturn = "";

            if (npc.currentState == NPCEntity.NPCState.DEAD)
            {
                messageToReturn += npc.GetLootMessage();
                List<ItemEntity> lootedItems = npc.GetLoot();

                foreach (ItemEntity item in lootedItems)
                {
                    messageToReturn += "\n" + player.AddItemToInventory(item);
                }

                world.RemoveNPCFromChunk(npc);
                return messageToReturn;
            }
            else
            {
                return npc.GetLootMessage();
            }
        }

        else
        {
            string[] targetArray = target.Split(" ");
            string t = "";

            foreach (string word in targetArray)
            {
                if (word != "Corpse")
                {
                    t += word;
                }
            }
            return "There isn't a " + t + " in the area.";
        }
   }

   public string Process_GetInventory()
   {
        return player.GetInventory();
   }

   public string Process_GetEquipment()
   {
        return player.GetEquippedItems();
   }

   public string Process_Equip(string target)
   {
        ItemEntity itemInInventory = player.GetItemFromInventory(target);

        if (itemInInventory != null && itemInInventory.entityType == ItemEntity.entTypes.EQUIPABLE)
        {
            player.RemoveItemFromInventory(target);
            return player.EquipItem(itemInInventory);
        }
        else if (itemInInventory != null && itemInInventory.entityType != ItemEntity.entTypes.EQUIPABLE)
        {
            return target + " cannot be equipped.";
        }
        else
        {
           return target + " isn't in your inventory.";
        }
   }
   
   public string Process_Unequip(string target)
    {
        string messageToReturn = "";

        ItemEntity item = player.UnequipItem(target);

        if (item != null)
        {
            messageToReturn += item.entityName + " has been unequiped.";
            messageToReturn += player.AddItemToInventory(item);
        }
        else
        {
            messageToReturn += "You don't have a " + target + " equipped.";
        }

        return messageToReturn;
    }

    public string Process_Drop(string target, int amount)
    {
        string messageToReturn = "";

        for (int i = 0; i<amount; i++)
        {
            ItemEntity item = player.GetItemFromInventory(target);

            if (item != null)
            {
                player.RemoveItemFromInventory(target);
                world.AddItemToCurrentChunk(item);

                messageToReturn = target + " has been dropped into the world.";
            }
            else if (item == null && i == 0)
            {
                messageToReturn = target + " doesn't exist in your inventory.";
            }
        }

        return messageToReturn;
    }

    public string Process_Consume(string target, int amount)
    {
        ItemEntity item = player.GetItemFromInventory(target);

        if (item != null)
        {
            int itemQuantity = player.GetItemQuantityInInventory(target);

            if (itemQuantity >= amount)
            {
                if (target == "oxygen")
                {
                    int itemUsed = player.AddOxygen(amount);
                    player.RemoveItemFromInventory(target, itemUsed);
                    return "You consume refill your oxygen level with " + itemUsed + " containers of oxygen.";
                }
                else
                {
                    return target + " cannot be consumed.";
                }
            }
        }
        else
        {
            return "You don't have any " + target + " in your pack.";
        }
        
        

        return target + " cannot be consumed.";
    }

    public string AddItemToEnvironment(string target, int amount, string subtarget)
    {
        ChunkData currentChunk = world.GetChunkAtWorldCoords();
        EnvironmentEntity environmentObj = actionHandler.GetEnvironmentObj(subtarget, currentChunk);

        if (environmentObj == null)
        {
            return subtarget + " doesn't exist in the area.";
        }

        ItemEntity item = actionHandler.GetItemInInventory(target);

        if (item == null)
        {
            return target + " doesn't exist in your pack.";
        }

        int quantityOH = player.GetItemQuantityInInventory(target);

        if (quantityOH >= amount)
        {   
            environmentObj.AddItemToInventory(item, amount);
            player.RemoveItemFromInventory(target, amount);
            return amount + " " + target + " has been removed from your pack and stored in the " + subtarget;
        }
        else
        {
            return "You don't have enough " + target + " to do that.";
        }
    }
}
