//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Nulgath/CoreNulgath.cs
using RBot;

public class SwindlesReturnPolicy
{
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public CoreNulgath Nulgath = new CoreNulgath();
    

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        Nulgath.SwindleReturn();

        Core.SetOptions(false);
    }
}