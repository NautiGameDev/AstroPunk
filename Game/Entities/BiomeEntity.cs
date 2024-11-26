/*
Biome Entity is used to parse biome data for specific biome to be used for generating a new chunk in the world.
*/

public class BiomeEntity
{
    public string biomeName;
    public int numbNPCObj;
    public int numbEnvironmentObj;
    public int numbItemObj;
    public float humidty;
    public float temperature;
    public float altitude;
    public string[] descriptions;
    public string[] npcEntities;
    public string[] itemEntities;
    public string[] environmentEntities;

    public BiomeEntity(BiomeData biomeData, string biome)
    {
        Dictionary<string, string> thisBiomeDict = biomeData.biomeDict[biome];

        biomeName = thisBiomeDict["Name"];
        numbNPCObj = int.Parse(thisBiomeDict["NumbNPCObj"]);
        numbEnvironmentObj = int.Parse(thisBiomeDict["NumbEnvironmentObj"]);
        numbItemObj = int.Parse(thisBiomeDict["NumbItemObj"]);
        humidty = float.Parse(thisBiomeDict["Humidity"]);
        temperature = float.Parse(thisBiomeDict["Temperature"]);
        altitude = float.Parse(thisBiomeDict["Altitude"]);
        descriptions =  thisBiomeDict["Description"].Split("/");
        npcEntities = thisBiomeDict["NPCEntities"].Split("/");
        environmentEntities = thisBiomeDict["EnvironmentEntities"].Split("/");
        itemEntities = thisBiomeDict["ItemEntities"].Split("/");
    }

}
