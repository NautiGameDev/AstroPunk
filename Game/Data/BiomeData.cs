using System;
using System.Collections;
using System.Collections.Generic;

public class BiomeData
{

    public Dictionary<string, Dictionary<string, string>> biomeDict = new Dictionary<string, Dictionary<string, string>>()
    {
        {"Test Biome", 
            new Dictionary<string, string>()
            {
                {"Name", "Test Biome"},
                {"NumbNPCObj", "1"},
                {"NumbEnvironmentObj","5"},
                {"NumbItemObj","3"},
                {"Humidity","0"},
                {"Temperature","0"},
                {"Altitude","0"},
                {"Description","You enter the void, a biome used to test anything and everything"},
                {"NPCEntities", "Griznak%1"},
                {"ItemEntities", "Stone%0.75/Stellar Seed%1"},
                {"EnvironmentEntities", "Coal Deposit%0.01/Uranium Deposit %0.5/Copper Deposit%0.06/Quartz Crystal%0.08/Iron Deposit%0.1/Gold Deposit%0.12/Silver Deposit%0.14/Titanium Deposit%0.16/Pond%0.03/Luminescent Fern%0.5/Stellar Arbor%1"}
            }
         }         
    };
}
        
    

