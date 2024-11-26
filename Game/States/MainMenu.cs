using AstroPunk;

class MainMenu
{
    Game game;

    public MainMenu(Game g)
    {
        game = g;
    }

    public string[] GetMessages()
    {
        string[] messagesToReturn = new string[2];
        messagesToReturn[0] = DisplayBanner();
        messagesToReturn[1] = DisplayContent();

        return messagesToReturn;
    }

    public string ProcessInput(string playerInput)
    {
        switch(playerInput.ToLower())
        {
            case "new game":
                game.ChangeState(Game.ProgramState.NEWGAME);
                break;

            case "news":
                game.ChangeState(Game.ProgramState.NEWS);
                break;

            case "quit":
                return "quit";
                
            default:
                return "Please enter a valid menu option.";
        }

        return "";
    }

    string DisplayBanner()
    {
        string banner = "--------------------------------------------------------------------------------------\n";
        return banner;
    }

    string DisplayContent()
    {
        string displayContent = "\nWelcome to AstroPunk! The current version is Alpha 0.0.1\n\n";
        

        List<string> menuOptions = new List<string> {"# New Game\n", "# News\n", "# Quit\n"};

        foreach (string option in menuOptions)
        {
            displayContent += option;
        }

        displayContent += "\nType the corresponding menu name to navigate the menu.(IE: New Game)\n\n";
        

        return displayContent;

    }
}