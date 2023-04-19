using System;

public class RepairCompanion : Collectible
{
    protected override void OnStartInteraction(object sender, EventArgs e)
    {
        QuestManager.instance.RepairedCompanion();
        base.OnStartInteraction(sender, e);
    }
}
