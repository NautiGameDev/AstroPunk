
using AstroPunk;

class News
{
    Dictionary<string, string> versionDictionary = new Dictionary<string, string>()
    {
        {"0.0.1", "\nAlpha version 0.0.1 Update Notes:\n\n# Established name for the game, titled AstroPunk. Previously Project TextCraft.\n# Created a stand-alone system, disconnected from Unity. Future updates will return to Unity for GUI.\n# Basic main menu created, including rough character creator for name and stats.\n# Common text adventure actions programmed into the game.\n# Crafting system started and functioning, although in its' infancy.\n# Rough procedural generation with basic items, entity objects, and NPCs for texting purposes.\n\nCurrent Production Focus: Flesh out crafting system with more complex objects and use of materials and items programmed into the game."}
    };

    Game game;
    string notesToDisplay = "";

    public News(Game g)
    {
        game = g;
    }

    public string[] GetMessages()
    {

        string[] messagesToReturn = new string[2];
        messagesToReturn[0] += DisplayBanner();

        if (notesToDisplay == "")
        {
            messagesToReturn[1] += DisplayVersionMenu();
        }
        else
        {
            messagesToReturn[1] += notesToDisplay;
            messagesToReturn[1] += "\n\nType back to return to update menu.\n";
        }

        return messagesToReturn;
    }

    public string ProcessInput(string playerInput)
    {
        string messageToReturn = "";

        if (notesToDisplay == "")
        {
            if (playerInput.ToLower() == "back")
            {
                game.ChangeState(Game.ProgramState.MAIN);
                return "";
            }

            try
            {
                notesToDisplay = versionDictionary[playerInput];
                return "";
            }
            catch
            {
                return playerInput + " isn't a valid update version.";
            }
        }
        else
        {
            if (playerInput.ToLower() == "back")
            {
                notesToDisplay = "";
                return "";
            }
            else
            {
                return "Can't process command " + playerInput;
            }
        }
    }

    string DisplayBanner()
    {
        string banner = "--------------------------------------------------------------------------------------\n";
        return banner;
    }

    string DisplayVersionMenu()
    {
        string message = "The current version of AstroPunk is Alpha 0.0.1.\n\nUpdate Notes:\n";
        

        foreach (string version in versionDictionary.Keys)
        {
            message += version + "\n";
        }

        message += "\nType the version number to see update notes for that version. Or type Back to return to main menu.\n";

        return message;
    }
}