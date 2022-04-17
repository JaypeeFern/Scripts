//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
using RBot;

public class HollowbornScythe
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        GetHBReapersScythe();

        Core.SetOptions(false);
    }

    public void GetHBReapersScythe()
    {
        if (Core.CheckInventory("Hollowborn Reaper's Scythe"))
            return;

        Core.AddDrop("Bone Dust", "Undead Energy");
        Farm.HollowbornREP(9);

        if (!Core.CheckInventory("Hollowborn Reaper's Minion"))
        {
            Core.Logger($"Farming for Hollowborn Reaper's Minion");
            Core.AddDrop("Hollowborn Reaper's Minion");

            Core.EquipClass(ClassType.Farm);
            Core.HuntMonster("shadowrealm", "Hollowborn Sentinel", "Hollow Soul", 250, false);
            Farm.BattleUnderB("Bone Dust", 2000);

            Core.EquipClass(ClassType.Solo);
            Core.KillMonster("shadowattack", "Boss", "Left", "Death", "Death's Oversight", 2, false, publicRoom: true);

            Core.BuyItem("shadowrealm", 1889, "Hollowborn Reaper's Minion");
        }

        foreach (string item in new[] { "Hollowborn Reaper's Daggers", "Hollowborn Reaper's Kamas", "Hollowborn Reaper's Kama" })
        {
            if (Core.CheckInventory(item))
                continue;

            Core.Logger($"Farming for {item}");

            Core.EquipClass(ClassType.Farm);
            Core.HuntMonster("shadowrealm", "Hollowborn Sentinel", "Hollow Soul", 250, false);
            Farm.BattleUnderB("Bone Dust", 3000);

            Core.EquipClass(ClassType.Solo);
            Core.KillMonster("shadowattack", "Boss", "Left", "Death", "Death's Oversight", 5, false, publicRoom: true);

            Core.Logger("Incarnation of Glitches Scythe (stop to buy back, ignore to farm)");
            Core.EquipClass(ClassType.Solo);
            Core.HuntMonster("cathedral", "Incarnation of Time", "Incarnation of Glitches Scythe", 1, false, publicRoom: true);

            if (!Core.CheckInventory("Unmoulded Fiend Essence"))
            {
                Farm.Gold(15000000);
                Core.BuyItem("tercessuinotlim", 1951, "Unmoulded Fiend Essence");
            }

            Core.BuyItem("shadowrealm", 1889, item);
        }

        Core.Logger("Incarnation of Glitches Scythe (stop to buy back, ignore to farm)");
        Core.EquipClass(ClassType.Solo);
        Core.HuntMonster("cathedral", "Incarnation of Time", "Incarnation of Glitches Scythe", 1, false, publicRoom: true);

        Core.BuyItem("shadowrealm", 1889, "Hollowborn Reaper's Scythe");
    }
}
