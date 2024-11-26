//Refactor this script.
//This script should only hande the terminal and printing text to the terminal.

//Key purposes of this script
//Get player input, send input to a text parsing system
//Get returned string to by printed to terminal, type each letter or print
//Clear terminal


namespace AstroPunk
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "AstroPunk";
            Console.ForegroundColor = ConsoleColor.White;

            Game game = new Game();

            bool isPlaying = true;
            bool isTesting = false;

            while (isTesting)
            {
                Parser textParser = new Parser();
                Console.Write("Input text >>");
                Dictionary<string, string> parsedInput = textParser.ParseInput(Console.ReadLine());

                foreach (string s in parsedInput.Keys)
                {
                    Console.WriteLine(s + " " + parsedInput[s]);
                }
    
                Console.WriteLine("\nPress any key to try again");
                Console.ReadKey();

            }

            while (isPlaying)
            {
                Console.Clear();

                string[] returnedMessages = game.GetMessagesToDisplay();
                PrintText(returnedMessages[0]);
                TypeText(returnedMessages[1]);

                TypeText("\n\nInput: ");
                string playerInput = Console.ReadLine();

                string returnedFromInput = game.ProcessInput(playerInput);

                if (returnedFromInput == "quit")
                {
                    isPlaying = false;
                    break;
                }

                if (returnedFromInput != "")
                {
                    PrintText(returnedFromInput);
                    PrintText("\nPress any key to continue...");
                    Console.ReadKey();
                }

            }
        }

        public static void PrintText(string text)
        {
            Console.Write(text);
        }

        public static void TypeText(string text, int speed=2)
        {
            foreach (char c in text)
            {
                Console.Write(c);
                Thread.Sleep(speed);
            }
        }

    }
}