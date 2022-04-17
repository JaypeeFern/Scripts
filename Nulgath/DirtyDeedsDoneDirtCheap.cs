//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Nulgath/CoreNulgath.cs
using RBot;
using System.Linq;

public class DirtyDeedsDoneDirtCheap
{
    public CoreBots Core => CoreBots.Instance;
    public CoreNulgath Nulgath = new CoreNulgath();
    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        Nulgath.DirtyDeedsDoneDirtCheap();

        Core.SetOptions(false);
    }
}