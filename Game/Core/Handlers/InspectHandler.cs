public class InspectHandler
{
    ActionHandler actionHandler;
    World world;

    public InspectHandler(ActionHandler AH)
    {
        actionHandler = AH;
        world = AH.Get_WorldReference();
    }

    public string Process_Inspect(string target)
    {
        ChunkData currentChunk = world.GetChunkAtWorldCoords();

        EnvironmentEntity environmentObj = actionHandler.GetEnvironmentObj(target, currentChunk);

        if (environmentObj != null)
        {
            return environmentObj.GetInspectMessage();
        }

        ItemEntity item = actionHandler.GetItemInWorld(target, currentChunk);

        if (item != null)
        {
            return item.GetInspectMessage();
        }

        NPCEntity npc = actionHandler.GetNPC(target, currentChunk);

        
        if (npc != null)
        {
            return npc.GetInspectMessage();
        }
        
        ItemEntity invItem = actionHandler.GetItemInInventory(target);

        if (invItem != null)
        {
            return invItem.GetInspectMessage();
        }

        ItemEntity eqItem = actionHandler.GetItemInEquipment(target);

        if (eqItem != null)
        {
            return eqItem.GetInspectMessage();
        }

        return "There isn't a " + target + " in the area.";

    }
}
