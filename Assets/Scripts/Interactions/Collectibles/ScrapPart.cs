using System;

public class ScrapPart : Collectible
{
    protected override void OnStartInteraction(object sender, EventArgs e)
    {
        if (!this.isActiveAndEnabled)
            return;

        QuestManager.instance.CollectedScrapPart();
        base.OnStartInteraction(sender, e);
    }
}
