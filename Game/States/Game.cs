using AstroPunk;

//This script interacts with the rest of the backend of the program.
//The script consists of two key methods: GetDisplayMessages() and ProcessPlayerInput()
//GetDisplayMessages() returns a string array of 2 strings built in program. 
//    The first string will be displayed immediately in the console (usually basic UI).
//    The second string will be typed to the console (What's happening in the game).
//ProcessPlayerInput takes input string from player input and sends it to appropriate menu.
//    This returns a string that is often blank "" if no errors have occured.
//    If an error has occured from user input, string is returned and typed at bottom of terminal

class Game
{
        MainMenu mainMenu;
        NewGame newGame;
        Play playGame;
        News news;

        public enum ProgramState {MAIN, NEWGAME, PLAY, LOAD, NEWS, QUIT};
        ProgramState currentState = ProgramState.MAIN;
        static Dictionary<string, string> playerStats;
        static string playerName;

        public Game()
        {
            mainMenu = new MainMenu(this);
            newGame = new NewGame(this);
            news = new News(this);
        }

        public string[] GetMessagesToDisplay()
        {
            string[] messagesToDisplay = new string[2];

            messagesToDisplay[0] += GetBanner();

            switch(currentState)
            {
                case ProgramState.MAIN:
                    string[] mainMenuMessages = mainMenu.GetMessages();
                    messagesToDisplay[0] += mainMenuMessages[0];
                    messagesToDisplay[1] += mainMenuMessages[1];
                    break;

                case ProgramState.NEWGAME:
                    string[] newGameMessages = newGame.GetMessages();
                    messagesToDisplay[0] += newGameMessages[0];
                    messagesToDisplay[1] += newGameMessages[1];
                    break;

                case ProgramState.PLAY:
                    string[] gamePlayMessages = playGame.GetMessages();
                    messagesToDisplay[0] += gamePlayMessages[0];
                    messagesToDisplay[1] += gamePlayMessages[1];
                    break;
                
                case ProgramState.LOAD:
                    //Temporarily removed until implemented
                    break;

                case ProgramState.NEWS:
                    string[] newsMessages = news.GetMessages();
                    messagesToDisplay[0] += newsMessages[0];
                    messagesToDisplay[1] += newsMessages[1];
                    break;
            }


            return messagesToDisplay;
        }

        public string ProcessInput(string playerInput)
        {
            if (playerInput == "load nauti")
            {
                LoadNauti();
                return "Loading developer character.";
            }

            switch(currentState)
            {
                case ProgramState.MAIN:
                    return mainMenu.ProcessInput(playerInput);

                case ProgramState.NEWGAME:
                    return newGame.ProcessInput(playerInput);

                case ProgramState.PLAY:
                    return playGame.ProcessInput(playerInput);
                
                case ProgramState.LOAD:
                    return "[Pause]";

                case ProgramState.NEWS:
                    return news.ProcessInput(playerInput);

                default:
                    return "";
            }
        }


        static string GetBanner()
        {
            List<string> bannerList = new List<string>()
            {
                "   ###     ######  ######## ########   #######  ########  ##     ## ##    ## ##    ## \n",
                "  ## ##   ##    ##    ##    ##     ## ##     ## ##     ## ##     ## ###   ## ##   ##  \n",
                " ##   ##  ##          ##    ##     ## ##     ## ##     ## ##     ## ####  ## ##  ##   \n",
                "##     ##  ######     ##    ########  ##     ## ########  ##     ## ## ## ## #####    \n",
                "#########       ##    ##    ##   ##   ##     ## ##        ##     ## ##  #### ##  ##   \n",
                "##     ## ##    ##    ##    ##    ##  ##     ## ##        ##     ## ##   ### ##   ##  \n",
                "##     ##  ######     ##    ##     ##  #######  ##         #######  ##    ## ##    ## \n"
            };

            string bannerString = "";

            foreach (string line in bannerList)
            {
                bannerString += line;
            }        

            return bannerString;
            
        }

        public void ChangeState(ProgramState state)
        {
            currentState = state;
        }

        public void StartNewGame(string pName, Dictionary<string, string> pStats)
        {
            playGame = new Play(pName, pStats);
            currentState = ProgramState.PLAY;
        }

        public void LoadNauti()
        {
            Dictionary<string, string> statsDict = new Dictionary<string, string>()
            {
                {"[STR]", "10"},
                {"[DEX]", "10"},
                {"[AGI]", "10"},
                {"[VIT]", "10"},
                {"[CHA]", "10"},
                {"[INT]", "10"},
            };

            playGame = new Play("Nauti", statsDict);
            currentState = ProgramState.PLAY;
        }
    }