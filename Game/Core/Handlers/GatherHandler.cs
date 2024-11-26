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

    public string Process_Gathering(string target)
    {
        ChunkData currentChunk = world.GetChunkAtWorldCoords();

        //Test if target is environment object
        EnvironmentEntity environmentObj = actionHandler.GetEnvironmentObj(target, currentChunk);

        if (environmentObj != null)
        {
            return Gather_Environment(environmentObj);
        }

        //Test if target is item in world
        ItemEntity item = actionHandler.GetItemInWorld(target, currentChunk);

        if (item != null)
        {
            return Gather_Item(item);
        }

        NPCEntity npc = actionHandler.GetNPC(target, currentChunk);

        if (npc != null)
        {
            return Gather_NPC(npc);
        }

        return "There isn't a " + target + " in the area.";
    }

    string Gather_Environment(EnvironmentEntity environmentObj)
    {
        ItemEntity gatheredItem = environmentObj.GetDropItem();
        world.AddItemToCurrentChunk(gatheredItem);

        if (environmentObj.entityType != EnvironmentEntity.entTypes.WATER && environmentObj.entityType != EnvironmentEntity.entTypes.FURNITURE)
        {
            world.RemoveEnvironmentFromChunk(environmentObj);
        }

        if (gatheredItem != null && environmentObj.entityType == EnvironmentEntity.entTypes.FURNITURE)
        {
            environmentObj.RemoveDropItem(gatheredItem);
        }

        if (gatheredItem == null)
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

    string Gather_NPC(NPCEntity npc)
    {
        return npc.GetGatherMessage();
    }
}
