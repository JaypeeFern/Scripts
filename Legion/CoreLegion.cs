using RBot;
using System.Windows.Forms;

public class CoreLegion
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public CoreStory Story = new CoreStory();
    public CoreAdvanced Adv = new CoreAdvanced();

    public string[] legionMedals =
    {
        "Legion Round 1 Medal",
        "Legion Round 2 Medal",
        "Legion Round 3 Medal",
        "Legion Round 4 Medal"
    };

    public void EmblemofDage(int quant = 500)
    {
        if (Core.CheckInventory("Emblem of Dage", quant))
            return;

        if (!Core.CheckInventory("Legion Round 4 Medal"))
            LegionRound4Medal();

        Core.AddDrop("Legion Seal", "Gem of Mastery");
        Core.EquipClass(ClassType.Farm);
        Adv.BestGear(GearBoost.dmgAll);
        Core.Logger($"Farming {quant} Emblems");
        int i = 1;
        while (!Bot.Inventory.Contains("Emblem of Dage", quant))
        {
            Core.EnsureAccept(4742);
            Core.KillMonster("shadowblast", "r10", "Left", "*", "Gem of Mastery", 1, false);
            Core.KillMonster("shadowblast", "r10", "Left", "*", "Legion Seal", 27, false);
            Core.EnsureComplete(4742);
            Bot.Wait.ForPickup("Emblem of Dage");
            Core.Logger($"Completed x{i++}");
        }
    }

    public void DarkToken(int quant = 600)
    {
        if (Core.CheckInventory("Dark Token", quant))
            return;

        int i = 1;
        Core.AddDrop("Dark Token");
        Core.Logger($"Farming {quant} Dark Tokens");
        Core.EquipClass(ClassType.Farm);
        Adv.SmartEnhance(Bot.Inventory.CurrentClass.Name);
        Adv.BestGear(GearBoost.Human);
        Core.Logger($"Starting off with {Bot.Inventory.GetQuantity("Dark Token")} Dark Tokens");
        while (!Bot.Inventory.Contains("Dark Token", quant))
        {
            Core.EnsureAccept(6248, 6249, 6251);
            Core.KillMonster("seraphicwardage", "r3", "Right", "*", "Seraphic Commanders Slain", 6, log: false);
            Core.EnsureComplete(6251);

            while (Bot.Inventory.ContainsTempItem("Seraphic Medals", 5))
                Core.ChainComplete(6248);
            while (Bot.Inventory.ContainsTempItem("Mega Seraphic Medals", 3))
                Core.ChainComplete(6249);
            Bot.Player.Pickup("Dark Token");
            Core.Logger($"{Bot.Inventory.GetQuantity("Dark Token")} Dark Tokens");
        }
    }

    public void DiamondTokenofDage(int quant = 300)
    {
        if (Core.CheckInventory("Diamond Token of Dage", quant))
            return;

        if (!Core.CheckInventory("Legion Round 4 Medal"))
            LegionRound4Medal();
        if (!Core.CheckInventory("Legion Token", 50))
            FarmLegionToken(50);

        Core.AddDrop("Diamond Token of Dage", "Legion Token");

        int i = 1;
        while (!Bot.Inventory.Contains("Diamond Token of Dage", quant))
        {
            Core.EnsureAccept(4743);
            if (!Core.CheckInventory("Defeated Makai", 25))
            {
                Core.EquipClass(ClassType.Farm);
                Core.KillMonster("tercessuinotlim", "m2", "Bottom", "Dark Makai", "Defeated Makai", 25, false);
            }
            Core.EquipClass(ClassType.Solo);
            Adv.BestGear(GearBoost.Chaos);
            Core.KillMonster("aqlesson", "Frame9", "Right", "Carnax", "Carnax Eye", publicRoom: true);
            Core.HuntMonster("deepchaos", "Kathool", "Kathool Tentacle", publicRoom: true);
            Core.KillMonster("dflesson", "r12", "Right", "Fluffy the Dracolich", "Fluffy's Bones", publicRoom: true);
            Adv.BestGear(GearBoost.Dragonkin);
            Core.HuntMonster("lair", "Red Dragon", "Red Dragon's Fang");
            Adv.BestGear(GearBoost.Human);
            Core.HuntMonster("bloodtitan", "Blood Titan", "Blood Titan's Blade", publicRoom: true);

            Core.EnsureComplete(4743);
            Bot.Player.Pickup("Legion Token", "Diamond Token of Dage");
            Core.Logger($"Completed x{i++}");
        }
    }

    /// <summary>
    /// Farms Legion Round 4 Medal in Shadow Blast Arena
    /// </summary>
    public void LegionRound4Medal()
    {
        if (Core.CheckInventory("Legion Round 4 Medal"))
            return;

        Core.AddDrop(legionMedals);
        Core.Logger("Farming Legion Round 4 Medal");
        Core.Join("shadowblast");
        Adv.BestGear(GearBoost.dmgAll);
        while (!Bot.Inventory.Contains("Legion Round 4 Medal"))
        {
            if (!Core.CheckInventory("Legion Round 1 Medal") &&
                !Core.CheckInventory("Legion Round 2 Medal") &&
                !Core.CheckInventory("Legion Round 3 Medal"))
            {
                Core.EnsureAccept(4738);
                Core.HuntMonster("shadowblast", "Caesaristhedark", "Nation Rookie Defeated", 5, true);
                Core.HuntMonster("shadowblast", "Shadowrise Guard", "Shadowscythe Rookie Defeated", 5, true);
                Core.EnsureComplete(4738);
                Bot.Wait.ForDrop("Legion Round 1 Medal");
                Core.Logger("Medal 1 acquired");
            }

            if (Core.CheckInventory("Legion Round 1 Medal"))
            {
                Core.EnsureAccept(4739);
                Core.HuntMonster("shadowblast", "Carnage", "Nation Veteran Defeated", 7, true);
                Core.HuntMonster("shadowblast", "Doombringer", "Shadowscythe Veteran Defeated", 7, true);
                Core.EnsureComplete(4739);
                Bot.Wait.ForDrop("Legion Round 2 Medal");
                Core.Logger("Medal 2 acquired");
            }

            if (Core.CheckInventory("Legion Round 2 Medal"))
            {
                Core.EnsureAccept(4740);
                Core.HuntMonster("shadowblast", "Minotaurofwar", "Nation Elite Defeated", 10, true);
                Core.HuntMonster("shadowblast", "Draconic Doomknight", "Shadowscythe Elite Defeated", 10, true);
                Core.EnsureComplete(4740);
                Bot.Wait.ForDrop("Legion Round 3 Medal");
                Core.Logger("Medal 3 acquired");
            }

            if (Core.CheckInventory("Legion Round 3 Medal"))
            {
                Core.EnsureAccept(4741);
                Core.HuntMonster("shadowblast", "Thanatos", "Thanatos Vanquished", 1, true);
                Core.EnsureComplete(4741);
                Bot.Wait.ForDrop("Legion Round 4 Medal");
                Core.Logger("Medal 4 acquired");
            }
        }
    }

    public void ApprovalAndFavor(int quantApproval = 5000, int quantFavor = 5000)
    {
        if (Core.CheckInventory("Dage's Approval", quantApproval) && Core.CheckInventory("Dage's Favor", quantFavor))
            return;

        Core.Logger($"Farming {quantApproval} Dage's Approval and {quantFavor} Dage's Favor");
        Core.Unbank("Dage's Approval", "Dage's Favor");
        Core.EquipClass(ClassType.Farm);
        Adv.BestGear(GearBoost.Undead);
        if (quantApproval > 0)
            Core.KillMonster("evilwardage", "r8", "Left", "*", "Dage's Approval", quantApproval, false);
        if (quantFavor > 0)
            Core.KillMonster("evilwardage", "r8", "Left", "*", "Dage's Favor", quantFavor, false);
    }

    public void BoneSigil(int quant = 1)
    {
        if (Core.CheckInventory("Bone Sigil", quant))
            return;

        Core.AddDrop("Bone Sigil");
        Adv.BestGear(GearBoost.Undead);
        while (!Core.CheckInventory("Bone Sigil", quant))
        {
            Core.EnsureAccept(6739);
            Core.HuntMonster("legionarena", "Legion Gladiator|Legion Sergeant", "Legion Grunt Defeated", 5);
            Core.EnsureComplete(6739);
            Bot.Wait.ForPickup("Bone Sigil");
        }
    }

    public void SoulForgeHammer()
    {
        if (Core.CheckInventory("SoulForge Hammer"))
            return;

        Core.AddDrop("SoulForge Hammer");
        Adv.BestGear(GearBoost.dmgAll);
        Core.EnsureAccept(2741);
        Core.HuntMonster("forest", "Zardman Grunt", "Zardman's StoneHammer", 1, false);
        Core.HuntMonster("shadowfall", "Skeletal Warrior", "Iron Hammer", 1, false);
        Core.HuntMonster("bludrut", "Rock Elemental", "Elemental Rock Hammer", 1, false);
        Core.EnsureComplete(2741);
        Bot.Wait.ForPickup("SoulForge Hammer");
    }

    public void FarmLegionToken(int quant = 25000)
    {
        if (Core.CheckInventory("Legion Token", quant))
            return;

        JoinLegion();

        LTBrightParagon(quant);
        LTArcaneParagon(quant);
        LTShogunParagon(quant);
        LTThanatosParagon(quant);
        LTFirstClassEntertainment(quant, true, 3);
        LTDreadrock(quant);
    }

    public void LTBrightParagon(int quant = 25000)
    {
        if (Core.CheckInventory("Legion Token", quant) || !Core.CheckInventory("Bright Paragon Pet"))
            return;

        JoinLegion();

        Core.AddDrop("Legion Token", "Legion Token Pile");
        Core.EquipClass(ClassType.Farm);
        Adv.BestGear(GearBoost.dmgAll);

        Core.Logger($"Farming Legion Tokens {quant - Bot.Inventory.GetQuantity("Legion Token")}/{quant} Legion Tokens");
        int i = 1;
        while (!Core.CheckInventory("Legion Token", quant))
        {
            Core.EnsureAccept(4704);
            Core.KillMonster("brightfortress", "r3", "Right", "*", "Badge of Loyalty", 10);
            Core.KillMonster("brightfortress", "r3", "Right", "*", "Badge of Corruption", 8);
            Core.KillMonster("brightfortress", "r3", "Right", "*", "Twisted Light Token", 6);
            Core.EnsureComplete(4704);
            Bot.Player.Pickup("Legion Token");
            Core.Logger($"Completed x{i++}");
        }
    }

    public void LTShogunParagon(int quant = 25000)
    {
        if (Core.CheckInventory("Legion Token", quant)
            || (!Core.CheckInventory("Shogun Paragon Pet") && !Core.CheckInventory("Paragon Fiend Quest Pet") && !Core.CheckInventory("Paragon Ringbearer") && !Core.CheckInventory("Shogun Dage Pet")))
            return;

        JoinLegion();

        Core.AddDrop("Legion Token");

        int QuestID = 0;
        if (Core.CheckInventory("Shogun Paragon Pet"))
            QuestID = 5755;
        else if (Core.CheckInventory("Shogun Dage Pet"))
            QuestID = 5756;
        else if (Core.CheckInventory("Paragon Fiend Quest Pet"))
        {
            if (Bot.Inventory.GetItemByName("Paragon Fiend Quest Pet").ID == 47578)
                QuestID = 6750;
            else if (Bot.Inventory.GetItemByName("Paragon Fiend Quest Pet").ID == 47614)
                QuestID = 6756;
        }
        else if (Core.CheckInventory("Paragon Ringbearer"))
            QuestID = 7073;

        Core.Logger($"Farming Legion Tokens {Bot.Inventory.GetQuantity("Legion Token")}/{quant}");
        int i = 1;
        Core.EquipClass(ClassType.Farm);
        Adv.BestGear(GearBoost.dmgAll);
        while (!Core.CheckInventory("Legion Token", quant))
        {
            Core.EnsureAccept(QuestID);
            Core.HuntMonster("fotia", "Fotia Elemental|Fotia Sprit", "Nothing Heard", 10);
            Core.HuntMonster("fotia", "Fotia Elemental|Fotia Sprit", "Nothing To See", 10);
            Core.HuntMonster("fotia", "Fotia Elemental|Fotia Sprit", "Area Secured and Quiet", 10);
            Core.EnsureComplete(QuestID);
            Bot.Player.Pickup("Legion Token");
            Core.Logger($"Completed x{i++}, Legion Tokens({Bot.Inventory.GetQuantity("Legion Token")}/{quant})");
        }
    }

    public void LTFirstClassEntertainment(int quant = 25000, bool onlyWithParty = false, int partySize = 4)
    {
        if (Core.CheckInventory("Legion Token", quant))
            return;

        JoinLegion();
        Adv.BestGear(GearBoost.Undead);

        Core.AddDrop("Legion Token");
        Core.Join("legionarena", publicRoom: true);
        if (Bot.Map.PlayerCount < partySize && onlyWithParty)
        {
            Core.Join("legionarena", ignoreCheck: true, publicRoom: true);
            if (Bot.Map.PlayerCount < partySize)
                return;
        }
        Core.EquipClass(ClassType.Solo);
        Bot.Player.Jump("Boss", "Left");
        int i = 1;
        while (!Core.CheckInventory("Legion Token", quant))
        {
            Core.EnsureAccept(6743);
            Core.HuntMonster("legionarena", "Legion Fiend Rider", "Axeros' Brooch");
            Core.EnsureComplete(6743);
            Bot.Player.Pickup("Legion Token");
            Core.Logger($"Completed x{i++}");
        }
    }

    public void LTDreadrock(int quant = 25000)
    {
        if (Core.CheckInventory("Legion Token", quant))
            return;

        JoinLegion();
        Core.BuyItem("underworld", 216, "Undead Champion");

        Core.AddDrop("Legion Token");
        Core.EquipClass(ClassType.Farm);
        Adv.BestGear(GearBoost.Human);

        Core.Logger($"Farming Legion Tokens {quant - Bot.Inventory.GetQuantity("Legion Token")}/{quant} Legion Tokens");
        Core.Join("dreadrock");
        int i = 1;
        while (!Core.CheckInventory("Legion Token", quant))
        {
            Core.EnsureAccept(4849);
            Core.KillMonster("dreadrock", "r3", "Bottom", "*", "Dreadrock Enemy Recruited", 6);
            Core.EnsureComplete(4849);
            Bot.Player.Pickup("Legion Token");
            Core.Logger($"Completed x{i++}");
        }
    }

    public void LTArcaneParagon(int quant = 25000)
    {
        if (Core.CheckInventory("Legion Token", quant) || !Core.CheckInventory("Arcane Paragon Pet"))
            return;

        JoinLegion();

        Core.AddDrop("Legion Token", "Granite Dracolich Soul", "Tempest Dracolich Soul", "Deluge Dracolich Soul", "Inferno Dracolich Soul");
        Core.EquipClass(ClassType.Farm);

        Core.Join("dragonheart");
        Adv.BestGear(GearBoost.Dragonkin);

        int i = 1;
        while (!Core.CheckInventory("Legion Token", quant))
        {
            Core.EnsureAccept(4896);

            Core.HuntMonster("dragonheart", "Granite Dracolich", "Granite Dracolich Soul", 4, isTemp: false);
            Core.HuntMonster("dragonheart", "Tempest Dracolich", "Tempest Dracolich Soul", 4, isTemp: false);
            Core.HuntMonster("dragonheart", "Inferno Dracolich", "Inferno Dracolich Soul", 4, isTemp: false);
            Core.HuntMonster("dragonheart", "Deluge Dracolich", "Deluge Dracolich Soul", 4, isTemp: false);
            Core.EnsureComplete(4896);
            Bot.Player.Pickup("Legion Token");
            Core.Logger($"Completed x{i++}");
        }
    }

    public void LTThanatosParagon(int quant = 25000)
    {
        if (Core.CheckInventory("Legion Token", quant) || !Core.CheckInventory("Thanatos Paragon Pet"))
            return;

        JoinLegion();

        Core.AddDrop("Legion Token");
        Core.EquipClass(ClassType.Farm);

        Core.Join("dragonheart");
        Adv.BestGear(GearBoost.Dragonkin);

        int i = 1;
        while (!Core.CheckInventory("Legion Token", quant))
        {
            Core.EnsureAccept(4100);
            Core.KillMonster("dragonheart", "r6", "Right", "Zombie Dragon", "Elemental Dragon Soul", 20);
            Core.EnsureComplete(4100);
            Bot.Player.Pickup("Legion Token");
            Core.Logger($"Completed x{i++}");
        }
    }

    public void JoinLegion()
    {
        if (Core.isCompletedBefore(793))
            return;

        Core.BuyItem("underworld", 215, "Undead Warrior");
        DialogResult SellUW = MessageBox.Show(
                                "Do you want the bot to sell the \"Undead Warrior\" armor after it has succesfully joined the legion. This will return 1080 AC to you",
                                "Sell \"Undead Warrior\"?",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question);

        Core.AddDrop("Ravaged Champion Soul");

        // Undead Champion Initiation
        Story.KillQuest(789, "greenguardwest", "Black Knight");

        // Mourn the Soldiers
        if (!Story.QuestProgression(790))
        {
            Core.EnsureAccept(790);
            Core.HuntMonster("dwarfhold", "Chaos Drow", "Chaos Drow slain");
            Core.HuntMonster("swordhavenundead", "Skeletal Soldier", "Skeletal Soldier slain");
            Core.HuntMonster("pirates", "Fishman Soldier", "Fishman Soldier slain");
            Core.HuntMonster("willowcreek", "Dwakel Soldier", "Dwakel Soldier slain");
            Core.EnsureComplete(790);
        }

        // Understanding Undead Champions
        Story.KillQuest(791, "battleunderb", "Undead Champion");

        // Player vs Power
        if (!Story.QuestProgression(792))
        {
            if (!Core.CheckInventory("Combat Trophy", 200))
                Farm.BludrutBrawlBoss(quant: 200);
            Story.ChainQuest(792);
        }

        // Fail to the King
        Story.KillQuest(793, "prison", "King Alteon's Knight");

        if (SellUW == DialogResult.Yes)
            Core.SellItem("Undead Warrior", all: true);
    }

    public void ObsidianRock(int quant)
    {
        if (Core.CheckInventory("Obsidian Rock", quant))
            return;

        Core.AddDrop("Obsidian Rock");

        Core.EquipClass(ClassType.Farm);
        while (!Core.CheckInventory("Obsidian Rock", 10))
        {
            Core.EnsureAccept(2742);
            Core.HuntMonster("hydra", "Fire Imp", "Obsidian Deposit", 10);
            Core.EnsureComplete(2742);
            Bot.Wait.ForPickup("Obsidian Rock");
        }
    }
}