

using System.Reflection.Metadata.Ecma335;
using System.Runtime;

class Parser
{

Dictionary<string, HashSet<string>> actions = new Dictionary<string, HashSet<string>>()
    {
        {"move", new HashSet<string> {"move", "go", "travel"}},
        {"north", new HashSet<string> {"north", "n"}},
        {"east", new HashSet<string> {"east", "e"}},
        {"west", new HashSet<string> {"west", "w"}},
        {"south", new HashSet<string> {"south", "s"}},
        {"get", new HashSet<string> {"harvest", "gather", "get", "g", "take", "grab", "loot"}},
        {"look", new HashSet<string> {"look", "l", "inspect", "examine", "view"}},
        {"listen", new HashSet<string> {"listen", "hear"}},
        {"attack", new HashSet<string> {"attack", "a", "kill", "murder", "fight", "destroy"}},
        {"eq", new HashSet<string> {"equipment", "eq", "equip", "wear", "wield"}},
        {"inv", new HashSet<string> {"inventory", "i", "inv", "pack"}},
        {"drop", new HashSet<string> {"drop", "d"}},
        {"unequip", new HashSet<string> {"unequip", "remove", "r"}},
        {"use", new HashSet<string> {"use", "utilize", "u"}},
        {"consume", new HashSet<string> {"consume", "drink", "eat", "c"}},
        {"status", new HashSet<string> {"diagnostics", "status"}},
        {"craft", new HashSet<string> {"craft", "make", "create", "m"}},
        {"refine", new HashSet<string> {"refine", "smelt"}},
        {"add", new HashSet<string> {"add", "insert", "put"}},
        {"/emote", new HashSet<string> {"/me", "/emote"}},
        {"/help", new HashSet<string> {"/help", "help", "guide", "tutorial", "how", "h"}}
    };


    public Dictionary<string, string> ParseInput(string playerInput)
    {
        //Split action from rest of string
        string[] splitAction = playerInput.Split(" ", 2);
        string action = splitAction[0].Trim();
        string coreAction = Get_Action(action);

        string amt = "1";
        string target = "";
        string subTarget = "";

        //Check for preposition and split string at preposition
        if (splitAction.Length > 1)
        {
            string[] splitPreposition = SplitAtPreposition(splitAction[1]);
        

            

            //Check for integers in string
            if (splitPreposition.Length == 0)
            {
                string[] separatedInt = SeparateInt(splitAction[1]);
                target = separatedInt[0].Trim();

                if (separatedInt[1] != null)
                {
                    amt = separatedInt[1].Trim();
                }
                
            }
            else
            {
                string[] separatedInt = SeparateInt(splitPreposition[0]);
                subTarget = splitPreposition[1].Trim();
                target = separatedInt[0].Trim();
                if (separatedInt[1] != null)
                {
                    amt = separatedInt[1].Trim();
                }
            }
        }

        Dictionary<string, string> parsedInput = new Dictionary<string, string>()
        {
            {"action", coreAction.ToLower()},
            {"target", target.ToLower()},
            {"amount", amt},
            {"subtarget", subTarget.ToLower()}
        };

        return parsedInput;
    }

    string[] SplitAtPreposition(string input)
    {
        
        if (input.Contains(" to "))
        {
            return input.Split(" to ");
        }
        else if (input.Contains(" at "))
        {
            return input.Split(" at ");
        }
        else if (input.Contains(" from "))
        {
            return input.Split(" from ");
        }
        else if (input.Contains(" with "))
        {
            return input.Split(" with ");
        }
        else if (input.Contains(" on "))
        {
            return input.Split(" on ");
        }
        else if (input.Contains(" onto "))
        {
            return input.Split(" onto ");
        }
        else if (input.Contains(" in "))
        {
            return input.Split(" in ");
        }
        else if (input.Contains(" into "))
        {
            return input.Split(" into ");
        }
        else if (input.Contains(" between "))
        {
            return input.Split(" between ");
        }
        else if (input.Contains(" by "))
        {
            return input.Split(" by ");
        }
        else if (input.Contains(" through "))
        {
            return input.Split(" through ");
        }
        else if (input.Contains(" against "))
        {
            return input.Split(" against ");
        }
        else if (input.Contains(" towards "))
        {
            return input.Split(" towards ");
        }
        else
        {
            return new string[0];
        }
    }

    string[] SeparateInt(string input)
    {
        string[] stringArray = new string[2];

        string[] splitTarget = input.Split(" ");
        foreach (string s in splitTarget)
        {
            try
            {
                int testInt = int.Parse(s);
                stringArray[1] = testInt.ToString();
            }
            catch
            {
                stringArray[0] += s + " ";
            }
        }

        return stringArray;
    }

    string Get_Action(string action)
    {
        foreach (var entry in actions)
        {
            if (entry.Value.Contains(action))
            {
                return entry.Key;
            }
        }

        return action;
    }
}