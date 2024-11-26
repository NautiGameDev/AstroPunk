using System;
using System.Collections;
using System.Collections.Generic;


public class PlayerData
{
    string playerName;
    string playerAge = "";

    //Change to enums for character creator
    string playerSex = "";
    string playerSkinTone = "";
    string playerHairColor = "";
    string playerHairType = "";
    string playerEyeColor = "";
    string playerFacialHair = "";


    //Base stats
    int playerVitality;
    int playerStrength;
    int playerDexterity;
    int playerAgility;
    int playerCharisma;
    int playerIntelligence;

    public PlayerData(string pName, int vitality, int strength, int dexterity, int agility, int charisma, int intelligence)
    {
        playerName = pName;
        playerVitality = vitality;
        playerStrength = strength;
        playerDexterity = dexterity;
        playerAgility = agility;
        playerCharisma = charisma;
        playerIntelligence = intelligence;

        if (playerVitality < 1) playerVitality = 1;
        if (playerStrength < 1) playerStrength = 1;
        if (playerDexterity < 1) playerDexterity = 1;
        if (playerAgility < 1) playerAgility = 1;
        if (playerCharisma < 1) playerCharisma = 1;
        if (playerIntelligence < 1) playerIntelligence = 1;
    }

    public Dictionary<string, string> GetDetails()
    {
        Dictionary<string, string> detailsDict = new Dictionary<string, string>();

        detailsDict.Add("Name", playerName);
        detailsDict.Add("Age", playerAge);
        detailsDict.Add("Sex", playerSex);
        detailsDict.Add("SkinTone", playerSkinTone);
        detailsDict.Add("HairColor", playerHairColor);
        detailsDict.Add("HairType", playerHairType);
        detailsDict.Add("EyeColor", playerEyeColor);
        detailsDict.Add("FacialHair", playerFacialHair);

        return detailsDict;
    }

    public Dictionary<string, int> GetStats()
    {
        Dictionary<string, int> statDict = new Dictionary<string, int>();

        statDict.Add("Vitality", playerVitality);
        statDict.Add("Strength", playerStrength);
        statDict.Add("Dexterity", playerDexterity);
        statDict.Add("Agility", playerAgility);
        statDict.Add("Charisma", playerCharisma);
        statDict.Add("Intelligence", playerIntelligence);

        return statDict;
    }

    public string GetPlayerName()
    {
        return playerName;
    }
}
