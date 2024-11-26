
public class CraftingData
{
    public Dictionary<string, Dictionary<string, string>> craftingRecipes = new Dictionary<string, Dictionary<string, string>> {
        {"Crude Refiner", 
            new Dictionary<string, string>{
                {"Equipment Required", "Crude Workbench"},
                {"Materials Required", "Stone%10/Carbon%5"}
            }
        },
        {"Crude Workbench", 
            new Dictionary<string, string>{
                {"Equipment Required", "None"},
                {"Materials Required", "Carbon%5"}
            }
        },
           
    };
}
