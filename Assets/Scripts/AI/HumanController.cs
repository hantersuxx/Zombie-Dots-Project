using System;

public class HumanController : StateController
{
    protected override void HandleDeath(object sender, EventArgs e)
    {
        base.HandleDeath(sender, e);
        SpawnParticles(Globals.HumanParticleHexColor);
    }
}
