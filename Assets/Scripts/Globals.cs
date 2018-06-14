public static class Globals
{
    public static int KillZombieScore { get; private set; } = 10;
    public static int KillBossScore { get; private set; } = 100;
    public static int KillHumanScore { get; private set; } = -15;
    public static int SaveHumanScore { get; private set; } = 10;
    public static int MissZombieScore { get; private set; } = -10;
    public static int FinishLevelMultiplier { get; private set; } = 100;

    public static int TapDuration { get; private set; } = 50;
    public static int VaultAttackDuration { get; private set; } = 60;

    public static string ZombieParticleHexColor { get; private set; } = "#ff0000";
    public static string HumanParticleHexColor { get; private set; } = "#00ff00";

    public static string LevelCollectionFileName { get; private set; } = $"{nameof(LevelCollectionFileName)}.json";
}