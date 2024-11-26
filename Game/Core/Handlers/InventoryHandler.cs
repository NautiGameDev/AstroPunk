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

    public string Process_Drop(string target)
    {
        ItemEntity item = player.GetItemFromInventory(target);

        if (item != null)
        {
            player.RemoveItemFromInventory(target);
            world.AddItemToCurrentChunk(item);

            return item.entityName + " has been dropped into the world.";
        }
        else
        {
            return target + " doesn't exist in your inventory.";
        }
    }

    public string Process_Consume(string target)
    {
        if (target == "oxygen")
        {
            ItemEntity o2Item = player.GetItemFromInventory(target);

            if (o2Item != null)
            {
                int o2Quantity = player.GetItemQuantityInInventory(target);

                if (o2Quantity > 0)
                {
                    int o2Used = player.AddOxygen(o2Quantity);
                    player.RemoveItemFromInventory(target, o2Used);
                    return "You refill your oxygen tank with containers from your inventory.\nOxygen Used: " + o2Used;
                }
            }
            else
            {
                return "You don't have any oxygen in your pack.";
            }
        }

        return target + " cannot be consumed.";
    }
}
