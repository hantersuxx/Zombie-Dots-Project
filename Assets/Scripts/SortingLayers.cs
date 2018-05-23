using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SortingLayers
{
    public static string Ground { get; set; } = nameof(Ground);
    public static string Obstruction { get; set; } = nameof(Obstruction);
    public static string Zombie { get; set; } = nameof(Zombie);
    public static string Human { get; set; } = nameof(Human);
    public static string Default { get; set; } = nameof(Default);
}