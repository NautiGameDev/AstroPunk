public class NPCData
{

    public Dictionary<string, Dictionary<string, string>> NPCDict = new Dictionary<string, Dictionary<string, string>>()
    {
        {"Griznak",
            new Dictionary<string, string>()
            {
                {"EntityName", "Griznak"},
                {"EntityRace", "Griznak"},
                {"Vitality", "8"},
                {"Strength", "5"},
                {"Dexterity", "5"},
                {"Agility", "5"},
                {"Charisma", "5"},
                {"DropTable", ""},
                {"Equipment", "Ragged Leggings/Ragged Shoulders/Ragged Boots/Plasma Pistol"},
                {"AttackMessage", "The griznak grunts in agony."},
                {"ListenMessage", "The griznak mumbles something in its' own language. You can't understand it."},
                {"InspectMessage", "A diminutive, hairless humanoid with oversized, almond-shaped black eyes. Its pale gray skin is taut, and a distinctive Griznak insignia is etched beneath its ocular cavity."},
                {"GatherMessage", "Though small, the griznak isn't going to fit in your pack."},
                {"LootMessage", "You can't loot the griznak while it's still alive."},
                {"DeadAttackMessage", "The griznak is already dead. You can't possible kill it further."},
                {"DeadListenMessage", "You listen to the dead griznak, but you don't hear anything"},
                {"DeadInspectMessage", "A diminutive, hairless humanoid with oversized, almond-shaped black eyes. Its pale gray skin is taut, and a distinctive Griznak insignia is etched beneath its ocular cavity."},
                {"DeadGatherMessage", "Though small, the griznak isn't going to fit in your pack."},
                {"DeadLootMessage", "You rummage the griznak's corpse and salvage any remaining items."}
            }
        }
    };

}
