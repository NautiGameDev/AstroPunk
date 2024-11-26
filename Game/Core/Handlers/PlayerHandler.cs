public class PlayerHandler
{
    ActionHandler actionHandler;
    Player player;
    World world;

    public PlayerHandler(ActionHandler AH)
    {
        actionHandler = AH;
        player = AH.Get_PlayerReference();
        world = AH.Get_WorldReference();
    }

    public string Process_Diagnostics()
    {
        string messageToReturn = "";
        messageToReturn += "Running diagnostic system...\n";
        int[] pHealth = player.GetPlayerHealth();
        int[] pOxygen = player.GetOxygen();

        messageToReturn += "Health: " + pHealth[0] + "/" + pHealth[1];
        messageToReturn += "\nOxygen: " + pOxygen[0] + "/" + pOxygen[1];

        return messageToReturn;
    }

    public string Process_Emote(string pInput)
    {
        string messageToReturn = player.GetPlayerName();
        messageToReturn += " " + pInput;
        

        return messageToReturn;
    }
}
