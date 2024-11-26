using System;
public static class Utilities
{
    public static Dictionary<string, float> GetStatScales()
    {
        Dictionary<string, float> statScales = new Dictionary<string, float>();

        statScales.Add("Vitality", 2);
        statScales.Add("Strength", 1);
        statScales.Add("Dexterity", 1);
        statScales.Add("Agility", 0.01f);
        statScales.Add("CarryWeight", 2);

        return statScales;
    }

    public static int GetRandomInt(int min, int max)
    {
        Random random = new Random();
        return random.Next(min, max);
    }

    public static float GetRandomFloat_0to1()
    {
        Random random = new Random();
        return (float)random.NextDouble();
    }
}
