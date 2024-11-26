public class MovementHandler
{
    World world;

    string previousBiome = "";

    public MovementHandler(ActionHandler AH)
    {
        world = AH.Get_WorldReference();
    }

    public string ProcessMovement(string target)
    {
        if (target == "")
        {
            return "Which direction do you want to travel?";
        }

        if (target == "north" || target == "n")
        {
            world.UpdatePosition(new int[2] {0,1});
            string biomeText = GetBiomeDescription();
            return "You travel north" + biomeText;
        }

        else if (target == "east" || target == "e")
        {
            world.UpdatePosition(new int[2] {1, 0});
            string biomeText = GetBiomeDescription();
            return "You travel east" + biomeText;
        }

        else if (target == "west" || target == "w")
        {
            world.UpdatePosition(new int[2] {-1, 0});
            string biomeText = GetBiomeDescription();
            return "You travel west" + biomeText;
        }

        else if (target == "south" || target == "s")
        {
            world.UpdatePosition(new int[2] {0, -1});
            string biomeText = GetBiomeDescription();
            return "You travel south " + biomeText;
        }

        return target + " isn't a valid direction to travel.";
    }

    string GetBiomeDescription()
    {
        ChunkData currentChunk = world.GetChunkAtWorldCoords();

        if (currentChunk.biomeName != previousBiome)
        {
            previousBiome = currentChunk.biomeName;
            return " into the " + currentChunk.biomeName + ". " + currentChunk.GetRandomDescription();
        }

        return ".";
    }
}
