using System;

public static class Globals
{
    public static int KillZombieScore => 10;
    public static int KillBossScore => 100;
    public static int KillHumanScore => -20;
    public static int SaveHumanScore => 10;
    public static int MissZombieScore => -10;
    public static int FinishLevelMultiplier => 100;

    public static int TapDuration => 50;
    public static int VaultAttackDuration => 60;

    public static string ZombieParticleHexColor => "#ff0000";
    public static string HumanParticleHexColor => "#00ff00";


    public static string FileExtension { get; private set; } = "json";
    public static string GetFilename<T>() => $"{typeof(T)}.{FileExtension}";

    public static string AdmobAppId => "ca-app-pub-4879527705302532~5109824288";
    public static string ZombieDotsBanner => "ca-app-pub-3940256099942544/6300978111";
    //public static string ZombieDotsBanner => "ca-app-pub-4879527705302532/2911461381";
}