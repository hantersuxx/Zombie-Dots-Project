using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SortingLayers
{
    public static string Ground { get; set; } = nameof(Ground);
    public static string AboveGround { get; set; } = nameof(AboveGround);
    public static string Obstruction { get; set; } = nameof(Obstruction);
    public static string Being { get; set; } = nameof(Being);
}