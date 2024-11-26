public class GatherHandler
{

    ActionHandler actionHandler;
    World world;
    Player player;

 
    public GatherHandler(ActionHandler AH)
    {
        world = AH.Get_WorldReference();
        player = AH.Get_PlayerReference();
        actionHandler = AH;    
    }

    public string Process_Gathering(string target, string amount, string subtarget)
    {
        ChunkData currentChunk = world.GetChunkAtWorldCoords();
        string messageToReturn = "";

        if (subtarget == "")
        {
            for (int i=0; i<int.Parse(amount); i++)
            {
                //Test if target is environment object
                EnvironmentEntity environmentObj = actionHandler.GetEnvironmentObj(target, currentChunk);

                if (environmentObj != null)
                {
                    messageToReturn = Gather_Environment(environmentObj);
                }
                else
                {
                    if (messageToReturn != "")
                    {
                        return messageToReturn;
                    }

                    break;
                }
            }
        }
        else
        {
            EnvironmentEntity environmentObj = actionHandler.GetEnvironmentObj(subtarget, currentChunk);

            if (environmentObj != null)
            {
                ItemEntity retrievedItem = environmentObj.RemoveItemFromInventory(target, int.Parse(amount));
                
                if (retrievedItem != null)
                {
                    return player.AddItemToInventory(retrievedItem, int.Parse(amount));
                }
            }
            else
            {
                return "You search for " + target + " in the " + subtarget + " but don't find any.";
            }
        }

        //Test if target is item in world
        for (int i=0; i<int.Parse(amount); i++)
        {
        
            ItemEntity item = actionHandler.GetItemInWorld(target, currentChunk);

            if (item != null)
            {
                messageToReturn = Gather_Item(item);
                  
            } 
            else
            {
                if (messageToReturn != "")
                {
                    return messageToReturn;
                }

                break;
            } 
        }


        for (int i=0; i<int.Parse(amount); i++)
        {
            NPCEntity npc = actionHandler.GetNPC(target, currentChunk);

            if (npc != null)
            {

                if (npc.currentState == NPCEntity.NPCState.DEAD)
                {
                    messageToReturn += "\n" + npc.GetLootMessage();
                    List<ItemEntity> lootedItems = npc.GetLoot();

                    foreach (ItemEntity lootedItem in lootedItems)
                    {
                        messageToReturn += "\n" + player.AddItemToInventory(lootedItem);
                    }

                    world.RemoveNPCFromChunk(npc);
                }
                else if (npc.currentState == NPCEntity.NPCState.ALIVE)
                {
                    messageToReturn = npc.GetLootMessage();
                }
            }

            else
            {

                if (messageToReturn != "")
                {
                    return messageToReturn;
                }
                
                break;
            }
        }

        if (messageToReturn != "")
        {
            return messageToReturn;
        }
        else
        {
            return "There isn't a " + target + " in the area.";
        }
    }

    string Gather_Environment(EnvironmentEntity environmentObj)
    {
        string messageToReturn = "";
        ItemEntity gatheredItem = environmentObj.GetDropItem();

        if (gatheredItem != null)
        {
            messageToReturn += gatheredItem.GetGatherMessage();
        }

        List<ItemEntity> storedItems = environmentObj.GetStoredItems();
        world.AddItemToCurrentChunk(gatheredItem);

        foreach (ItemEntity item in storedItems)
        {
            world.AddItemToCurrentChunk(item);
            messageToReturn += "\n A hidden " + item + " has fallen onto the ground.";
        }

        if (environmentObj.entityType != EnvironmentEntity.entTypes.WATER && environmentObj.entityType != EnvironmentEntity.entTypes.FURNITURE)
        {
            world.RemoveEnvironmentFromChunk(environmentObj);
        }

        if (gatheredItem == null && storedItems.Count == 0)
        {
            return "There isn't anything worth gathering.";
        }



        return environmentObj.GetGatherMessage();
    }

    string Gather_Item(ItemEntity item)
    {
        string messageToReturn = item.GetGatherMessage();
        messageToReturn += player.AddItemToInventory(item);
        world.RemoveItemFromChunk(item);
        return messageToReturn;
    }
}
