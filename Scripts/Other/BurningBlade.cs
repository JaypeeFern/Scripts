//cs_include Scripts/CoreBots.cs
using RBot;

public class BurningBlade
{
    public ScriptInterface Bot => ScriptInterface.Instance;

    public CoreBots Core => CoreBots.Instance;

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();
        GetBurningBlade();
        Core.SetOptions(false);
    }

    public void GetBurningBlade()
    {
        if (Core.CheckInventory("Burning Blade"))
        {
            Core.Logger("You already own the Burning Blade.");
            return;
        }
        Core.KillMonster("lostruinswar", "r7", "Left", "Diabolical Warlord", "Burning Blade", isTemp: false, publicRoom: true);
    }
}