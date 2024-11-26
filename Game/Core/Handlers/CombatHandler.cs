public class CombatHandler
{
    ActionHandler actionHandler;
    World world;
    Player player;

    public CombatHandler(ActionHandler AH)
    {
        actionHandler = AH;
        world = AH.Get_WorldReference();
        player = AH.Get_PlayerReference();
    }

    public string Process_Combat(string target)
    {
        ChunkData currentChunk = world.GetChunkAtWorldCoords();

        EnvironmentEntity environmentObj = actionHandler.GetEnvironmentObj(target, currentChunk);

        if (environmentObj != null)
        {
            return environmentObj.GetAttackMessage();
        }

        ItemEntity item = actionHandler.GetItemInWorld(target, currentChunk);

        if (item != null)
        {
            return item.GetAttackMessage();
        }

        NPCEntity npc = actionHandler.GetNPC(target, currentChunk);

        if (npc != null)
        {
            return CombatAgainstNPC(npc);
        }

        ItemEntity itemFromInv = actionHandler.GetItemInInventory(target);

        if (item != null)
        {
            return itemFromInv.GetAttackMessage();
        }

        ItemEntity itemFromEq = actionHandler.GetItemInEquipment(target);

        if (item != null)
        {
            return itemFromInv.GetAttackMessage();
        }


        return "There isn't a " + target + " in the area.";
    }

    string CombatAgainstNPC(NPCEntity npc)
    {
        string messageToReturn = "";

        if (npc.IsAlive())
        {
            Dictionary<string, string> playerTurn = GetPlayerDamage(npc.entityName);

            int damageMinusArmor = int.Parse(playerTurn["RollDamage"]) + int.Parse(playerTurn["BaseDamage"]) - npc.GetArmorStat();

            messageToReturn += playerTurn["Message"] + " and deal " + damageMinusArmor + " damage.";
            messageToReturn += "\n{Roll Damage[" + playerTurn["NumberOfRolls"] + "d12]: " + playerTurn["RollDamage"] + "}";
            messageToReturn += " + {Base Damage: " + playerTurn["BaseDamage"] + "}";
            messageToReturn += " - {Damage Blocked: " + npc.GetArmorStat() + "}\n";
            messageToReturn += npc.GetAttackMessage();

            int[] npcHealth = npc.ResolveDamage(damageMinusArmor);

            messageToReturn += "\nNPC Health: " + npcHealth[0] + "/" + npcHealth[1] + "\n\n";

            int[] playerHealth = player.GetPlayerHealth();

            if (npcHealth[0] > 0)
            {
                Dictionary<string, string> npcTurn = GetNPCDamage(npc);

                int npcDamageMinusArmor = int.Parse(npcTurn["RollDamage"]) + int.Parse(npcTurn["BaseDamage"]) - player.GetArmorStat();

                messageToReturn += npc.entityName + " realiates against you and hits you for " + npcDamageMinusArmor + ".\n";
                messageToReturn += "\n{Roll Damage[" + npcTurn["NumberOfRolls"] + "d12]: " + npcTurn["RollDamage"] + "}";
                messageToReturn += " + {Base Damage: " + npcTurn["BaseDamage"] + "} - {Damage Blocked: " + player.GetArmorStat() + "}\n";

                playerHealth = player.ResolveDamage(npcDamageMinusArmor);

                messageToReturn += "\nYour health: " + playerHealth[0] + "/" + playerHealth[1];
            }
            else
            {
                messageToReturn += " It dies with a final breath, and its' lifeless corpse falls to the ground.";
                npc.currentState = NPCEntity.NPCState.DEAD;
                npc.entityName += " Corpse";
            }

            if (playerHealth[0] <= 0)
            {
                messageToReturn += "\n\nYou meet an unfortunate fate and become deceased";
                player.RespawnPlayer();
                messageToReturn += "\nYou respawn at your last saved respawn point.";
            }

        }
        else
        {
            messageToReturn += npc.GetAttackMessage();
        }

        return messageToReturn;
    }

    Dictionary<string, string> GetPlayerDamage(string target)
    {
        Dictionary<string, string> GPD = new Dictionary<string, string>()
        {
            {"Message", ""},
            {"RollDamage", ""},
            {"BaseDamage", ""},
            {"NumberOfRolls", ""}
        };

        ItemEntity playerPrimary = player.GetEquippedItemInSlot("PrimaryWeaponSlot");
        ItemEntity playerSecondary = player.GetEquippedItemInSlot("SecondaryWeaponSlot");

        int numberOfRolls = 0;
        int baseDamage = 0;

        if (playerPrimary != null)
        {
            numberOfRolls = playerPrimary.ATKDiceNumber;
            baseDamage = player.GetMeleeDamage();
            GPD["Message"] += GetAttackDescription(playerPrimary, target);
        }
        else if (playerSecondary != null)
        {
            numberOfRolls = playerSecondary.ATKDiceNumber;
            baseDamage = player.GetRangeDamage();
            GPD["Message"] += GetAttackDescription(playerSecondary, target);
        }
        else
        {
            numberOfRolls = 1;
            baseDamage = player.GetMeleeDamage();
            GPD["Message"] += "You swing your fist at the " + target;
        }

        int rollDamage = RollForDamage(numberOfRolls);

        GPD["RollDamage"] = rollDamage.ToString();
        GPD["BaseDamage"] = baseDamage.ToString();
        GPD["NumberOfRolls"] = numberOfRolls.ToString();

        return GPD;
    }

    Dictionary<string, string> GetNPCDamage(NPCEntity npc)
    {
        Dictionary<string, string> NPCDamage = new Dictionary<string, string>(){
            {"RollDamage", ""},
            {"BaseDamage", ""},
            {"NumberOfRolls", ""}
        };

        int npcDiceRolls = npc.GetWeaponDice();
        int npcRollDmg = RollForDamage(npcDiceRolls);
        int npcBaseDmg = npc.GetBaseDamage();

        NPCDamage["NumberOfRolls"] = npcDiceRolls.ToString();
        NPCDamage["RollDamage"] = npcRollDmg.ToString();
        NPCDamage["BaseDamage"] = npcBaseDmg.ToString();

        return NPCDamage;
    }

    string GetAttackDescription(ItemEntity weapon, string target)
    {
        if (weapon.GetRange() == 1)
        {
            return "You swing your " + weapon.entityName + " at the " + target;
        }
        else if (weapon.GetRange() == 2)
        {
            return "You carefully aim and shoot your " + weapon.entityName + " at the " + target;
        }
        else
        {
            return "";
        }
    }

    int RollForDamage(int numberOfRolls)
    {
        int damage = 0;

        for (int i=0; i< numberOfRolls; i++)
        {
            int randDmg = Utilities.GetRandomInt(1, 12);
            damage += randDmg;
        }

        return damage;
    }

}
