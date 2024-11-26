public class ListenHandler
{
    ActionHandler actionHandler;
    World world;

    public ListenHandler(ActionHandler AH)
    {
        actionHandler = AH;
        world = AH.Get_WorldReference();
    }

    public string Process_Listen(string target)
    {
        ChunkData currentChunk = world.GetChunkAtWorldCoords();

        EnvironmentEntity environmentObj = actionHandler.GetEnvironmentObj(target, currentChunk);

        if (environmentObj != null)
        {
            return environmentObj.GetListenMessage();
        }

        ItemEntity item = actionHandler.GetItemInWorld(target, currentChunk);

        if (item != null)
        {
            return item.GetListenMessage();
        }

        NPCEntity npc = actionHandler.GetNPC(target, currentChunk);

        
        if (npc != null)
        {
            return npc.GetListenMessage();
        }
        
        ItemEntity invItem = actionHandler.GetItemInInventory(target);

        if (invItem != null)
        {
            return invItem.GetListenMessage();
        }

        ItemEntity eqItem = actionHandler.GetItemInEquipment(target);

        if (eqItem != null)
        {
            return eqItem.GetListenMessage();
        }

        return "There isn't a " + target + " in the area.";

    }
}
