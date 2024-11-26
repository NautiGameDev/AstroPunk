using AstroPunk;

class NewGame
{
    string playerName = "";
    bool hasStats = false;
    int playerPoints = 30;

    Game game;

    Dictionary<string, string> statsDict = new Dictionary<string, string>()
    {
        {"[STR]", "1"},
        {"[DEX]", "1"},
        {"[AGI]", "1"},
        {"[VIT]", "1"},
        {"[CHA]", "1"},
        {"[INT]", "1"},
    };
    
    public NewGame(Game g)
    {
        game = g;
    }

    public string[] GetMessages()
    {
        string[] messagesToReturn = new string[2];

        messagesToReturn[0] += DisplayBanner();

        if (playerName == "")
        {
            messagesToReturn[1] = WelcomeMessage();
        }
        else if (!hasStats)
        {
            messagesToReturn[1] = PlayerStatsMessage();
        }

        return messagesToReturn;
    }

    public string ProcessInput(string playerInput)
    {
        if (playerName == "")
        {
            if (playerInput == "")
            {
                return "Please enter your name.";
            }

            playerName = playerInput;
            return "";
        }
        else if (!hasStats)
        {
            return ParseInput(playerInput);
        }

        return "";
    }

    string DisplayBanner()
    {
        string banner = "--------------------------------------------------------------------------------------\n";
        return banner;
    }

    string WelcomeMessage()
    {
        string welcome = "\nWelcome, earthling. Before we get started, please enter your name into the terminal.\n\n";
        return welcome;
    }

    string PlayerStatsMessage()
    {
        string statsMessage = "\nYour name: " + playerName + "\n\nNow let's set your character stats.\n\n";
        

        statsMessage+= "++++++++++++++++++++++++++++++++\n";

        foreach (string key in statsDict.Keys)
        {
            statsMessage += key;
            statsMessage += " " + statsDict[key] + "\n";
        }

        statsMessage += "++++++++++++++++++++++++++++++++\n";

        statsMessage += "\nYou have " + playerPoints + " left to spend.\n";

        statsMessage += "\nType the three corresponding letters for a skill, then +/- amount. (Example: str +5)\nType Done when you are finished.\n";

        return statsMessage;
    }



    string ParseInput(string playerInput)
    {
        if (playerInput.ToLower() == "done" && playerPoints == 0)
        {
            hasStats = true;
            game.StartNewGame(playerName, statsDict);
            playerName = "";
            statsDict["[STR]"] = "1";
            statsDict["[DEX]"] = "1";
            statsDict["[VIT]"] = "1";
            statsDict["[AGI]"] = "1";
            statsDict["[CHA]"] = "1";
            statsDict["[INT]"] = "1";
            return "";
        }
        if (playerInput.ToLower() == "done" && playerPoints > 0)
        {
            return "\nYou still have " + playerPoints + " points to spend!\n";
        }

        string[] parsedInput = playerInput.Split(" ");
        string stat = parsedInput[0].ToLower();
        string operation = "";
        string amount = "";
        
        foreach (char c in parsedInput[1])
        {
            string input = c.ToString();

            if (input == "+" || input == "-")
            {
                operation = input;
            }
            else
            {
                amount += input;
            }
        }

        if (int.Parse(amount) > playerPoints && operation == "+")
        {
            return "\nYou don't have enough points to do that.\n";
        }
        if (int.Parse(amount) > playerPoints && operation == "")
        {
            return "\nYou don't have enough points to do that.\n";
        }

        switch(stat)
        {
            case "str":
                return ModifyStat("[STR]", operation, amount);
            case "dex":
                return ModifyStat("[DEX]", operation, amount);
            case "agi":
                return ModifyStat("[AGI]", operation, amount);
            case "vit":
                return ModifyStat("[VIT]", operation, amount);
            case "cha":
                return ModifyStat("[CHA]", operation, amount);
            case "int":
                return ModifyStat("[INT]", operation, amount);
            default:
                return "\nThe input " + stat + " doesn't match any available stats.";
        }

    }

    string ModifyStat(string stat, string operation, string amount)
    {
        if (operation == "+" || operation == "")
        {
            int newStat = int.Parse(statsDict[stat]);
            newStat += int.Parse(amount);
            statsDict[stat] = newStat.ToString();
            playerPoints -= int.Parse(amount);
            return "";
        }

        else if (operation == "-")
        {
            if (int.Parse(statsDict[stat]) - int.Parse(amount) >= 1)
            {
                int newStat = int.Parse(statsDict[stat]);
                newStat -= int.Parse(amount);
                statsDict[stat] = newStat.ToString();
                playerPoints += int.Parse(amount);
                return "";
            }
            else
            {
                return "You don't have enough points in " + stat + " to remove that many.\nPress any key to retry...";
            }
        }

        return "";
    }
}