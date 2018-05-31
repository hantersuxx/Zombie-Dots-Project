using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Globals
{
    public static int KillZombieScore { get; private set; } = 10;
    public static int KillHumanScore { get; private set; } = -10;
    public static int SaveHumanScore { get; private set; } = 10;
    public static int MissZombieScore { get; private set; } = -10;
    public static string ZombieParticleHexColor { get; private set; } = "#ff0000";
    public static string HumanParticleHexColor { get; private set; } = "#00ff00";
}