public class HumanController : StateController
{
    protected override void OnDeath()
    {
        base.OnDeath();
        for (int i = 0; i < ParticleCount; i++)
        {
            ObjectPooler.Instance.SpawnFromPool(Tags.CreatureParticle, transform.position, Globals.HumanParticleHexColor);
        }
    }

    public override void OnObjectSpawn(object transferValue)
    {
        base.OnObjectSpawn(transferValue);
    }

    public override void Destroy()
    {
        base.Destroy();
    }
}
