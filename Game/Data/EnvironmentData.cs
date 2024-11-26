
public class EnvironmentData
{
    public Dictionary<string, Dictionary<string, string>> EnvironmentDict = new Dictionary<string, Dictionary<string, string>>()
    {

#region PLANT
        {"Stellar Arbor", 
            new Dictionary<string, string>() {
                {"EntityName", "Stellar Arbor"},
                {"EntityType", "Plant"},
                {"Drop Table", "Carbon"},
                {"Inventory", ""},
                {"AttackMessage", "You attack the stellar arbor like a lunatic, but nothing happens."},
                {"ListenMessage", "You listen to the stellar arbor. The deformed leaves rustle gently in the breeze."},
                {"InspectMessage", "A tall tree grown from a sapling. The bark is teal, rough, and full of imperfections. Green leaves fill the canopy above providing shade to the ground below."},
                {"GatherMessage", "You aim your molecular disassembler at the Stellar Arbor and fire a bright red lazer pulse. Within seconds the tree dissentigrates into carbon."},
                {"LootMessage", "There isn't anything that seems worth taking."}
            }
        },
        {"Luminescent Fern",
            new Dictionary<string, string>() {
                {"EntityName", "Luminescent Fern"},
                {"EntityType", "Plant"},
                {"Drop Table", "Oxygen"},
                {"Inventory", ""},
                {"AttackMessage", "You attack the Luminescent Fern. A few leaves are destroyed in the aftermath."},
                {"ListenMessage", "You listen to the Luminescent Fern, but don't hear anything."},
                {"InspectMessage", "A small green fern with a bluish glow."},
                {"GatherMessage", "You make quick work of the Luminescent Fern with your molecular disassembler. A red lazer pulses through the air and dissentegrates the plant into oxygen."},
                {"LootMessage", "There's nothing worth taking on the surface."}
            }
        },
#endregion

#region ORE
        {"Iron Deposit",
            new Dictionary<string, string>() {
                {"EntityName", "Iron Deposit"},
                {"EntityType", "Ore"},
                {"Drop Table", "Iron"},
                {"Inventory", ""},
                {"AttackMessage", "You attack the iron deposit with all your might, but nothing happens."},
                {"ListenMessage", "You listen to the iron deposit closely, but don't hear anything."},
                {"InspectMessage", "A rock structure with crude iron ore burried inside."},
                {"GatherMessage", "With your finger pressed on the trigger of your molecular disassembler, a blue beam shoots out and dissentigrates the rocks, leaving behind chunks of raw iron."},
                {"LootMessage", "There doesn't seem to be anything worth taking. Try harvesting instead."}
            }
        },
        {"Coal Deposit",
            new Dictionary<string, string>() {
                {"EntityName", "Coal Deposit"},
                {"EntityType", "Ore"},
                {"Drop Table", "Carbon"},
                {"Inventory", ""},
                {"AttackMessage", "You attack the coal deposit with all your might, but nothing happens."},
                {"ListenMessage", "You listen to the coal deposit closely, but don't hear anything."},
                {"InspectMessage", "A rock structure with coal burried inside."},
                {"GatherMessage", "With your finger pressed on the trigger of your molecular disassembler, a blue beam shoots out and dissentigrates the deposit, leaving behind the carbon for collection."},
                {"LootMessage", "There doesn't seem to be anything worth taking. Try harvesting instead."}
            }
        },
        {"Copper Deposit",
            new Dictionary<string, string>() {
                {"EntityName", "Copper Deposit"},
                {"EntityType", "Ore"},
                {"Drop Table", "Copper"},
                {"Inventory", ""},
                {"AttackMessage", "You attack the copper deposit with all your might, but nothing happens."},
                {"ListenMessage", "You listen to the copper deposit closely, but don't hear anything."},
                {"InspectMessage", "A rock structure with copper ore burried inside."},
                {"GatherMessage", "With your finger pressed on the trigger of your molecular disassembler, a blue beam shoots out and dissentigrates the deposit, leaving behind the copper for collection."},
                {"LootMessage", "There doesn't seem to be anything worth taking. Try harvesting instead."}
            }
        },
        {"Gold Deposit",
            new Dictionary<string, string>() {
                {"EntityName", "Gold Deposit"},
                {"EntityType", "Ore"},
                {"Drop Table", "Gold"},
                {"Inventory", ""},
                {"AttackMessage", "You attack the gold deposit with all your might, but nothing happens."},
                {"ListenMessage", "You listen to the gold deposit closely, but don't hear anything."},
                {"InspectMessage", "A rock structure with gold ore burried inside."},
                {"GatherMessage", "With your finger pressed on the trigger of your molecular disassembler, a blue beam shoots out and dissentigrates the deposit, leaving behind the gold for collection."},
                {"LootMessage", "There doesn't seem to be anything worth taking. Try harvesting instead."}
            }
        },
        {"Silver Deposit",
            new Dictionary<string, string>() {
                {"EntityName", "Silver Deposit"},
                {"EntityType", "Ore"},
                {"Drop Table", "Silver"},
                {"Inventory", ""},
                {"AttackMessage", "You attack the silver deposit with all your might, but nothing happens."},
                {"ListenMessage", "You listen to the silver deposit closely, but don't hear anything."},
                {"InspectMessage", "A rock structure with silver burried inside."},
                {"GatherMessage", "With your finger pressed on the trigger of your molecular disassembler, a blue beam shoots out and dissentigrates the deposit, leaving behind the silver for collection."},
                {"LootMessage", "There doesn't seem to be anything worth taking. Try harvesting instead."}
            }
        },
        {"Titanium Deposit",
            new Dictionary<string, string>() {
                {"EntityName", "Titanium Deposit"},
                {"EntityType", "Ore"},
                {"Drop Table", "Titanium"},
                {"Inventory", ""},
                {"AttackMessage", "You attack the titanium deposit with all your might, but nothing happens."},
                {"ListenMessage", "You listen to the titanium deposit closely, but don't hear anything."},
                {"InspectMessage", "A rock structure with titanium ore burried inside."},
                {"GatherMessage", "With your finger pressed on the trigger of your molecular disassembler, a blue beam shoots out and dissentigrates the deposit, leaving behind the titanium for collection."},
                {"LootMessage", "There doesn't seem to be anything worth taking. Try harvesting instead."}
            }
        },
        {"Uranium Deposit",
            new Dictionary<string, string>() {
                {"EntityName", "Uranium Deposit"},
                {"EntityType", "Ore"},
                {"Drop Table", "Uranium"},
                {"Inventory", ""},
                {"AttackMessage", "You attack the uranium deposit with all your might, but nothing happens."},
                {"ListenMessage", "You listen to the uranium deposit closely, but don't hear anything."},
                {"InspectMessage", "A rock structure with uranium ore burried inside."},
                {"GatherMessage", "With your finger pressed on the trigger of your molecular disassembler, a blue beam shoots out and dissentigrates the deposit, leaving behind the uranium for collection."},
                {"LootMessage", "There doesn't seem to be anything worth taking. Try harvesting instead."}
            }
        },
        {"Quartz Crystal",
            new Dictionary<string, string>() {
                {"EntityName", "Quartz Crystal"},
                {"EntityType", "Ore"},
                {"Drop Table", "Quartz"},
                {"Inventory", ""},
                {"AttackMessage", "You attack the quartz crystal with all your might, but nothing happens."},
                {"ListenMessage", "You listen to the quartz crystal closely, but don't hear anything."},
                {"InspectMessage", "A large crystal structure made of quartz."},
                {"GatherMessage", "With your finger pressed on the trigger of your molecular disassembler, a blue beam shoots out and dissentigrates the crystal, leaving behind the quartz for collection."},
                {"LootMessage", "There doesn't seem to be anything worth taking. Try harvesting instead."}
            }
        },
#endregion

#region WATER
        {"Pond",
            new Dictionary<string, string>() {
                {"EntityName", "Pond"},
                {"EntityType", "Water"},
                {"Drop Table", "Crystal Carp"},
                {"Inventory", ""},
                {"AttackMessage", "You attack the pond viciously. It accomplishes nothing, but you feel better inside."},
                {"ListenMessage", "You listen to the pond. There's a gentle sound from the waves crashing along the edge."},
                {"InspectMessage", "A small body of fresh water."},
                {"GatherMessage", "A green shockwave shoots from your molecular disassembler at the pond. The force creates a big enough splash that a fish is flung to the ground."},
                {"LootMessage", "There's nothing worth looting"}
            }
        },
#endregion

#region FURNITURE

        {"Crude Refiner",
            new Dictionary<string, string>() {
                {"EntityName", "Crude Refiner"},
                {"EntityType", "Furniture"},
                {"Drop Table", ""},
                {"Inventory", "Carbon%5"},
                {"AttackMessage", "You ponder attacking the refiner, but realize it won't do anything."},
                {"ListenMessage", "You listen to the refiner. A sound of subtle humming echoes from the machine."},
                {"InspectMessage", "A basic machine used to refine raw metals into ingots."},
                {"GatherMessage", "You search the refiner for unused fuel."},
                {"LootMessage", ""}
            }
        },
        {"Crude Workbench",
            new Dictionary<string, string>() {
                {"EntityName", "Crude Workbench"},
                {"EntityType", "Furniture"},
                {"Drop Table", ""},
                {"Inventory", ""},
                {"AttackMessage", "Attacking the workbench would only hurt you in the long run."},
                {"ListenMessage", "You listen to the workbench. Nothing happens."},
                {"InspectMessage", "A crude wooden table used for crafting basic items."},
                {"GatherMessage", "There's nothing to gather."},
                {"LootMessage", ""}
            }
        },
        

#endregion

    };
}
