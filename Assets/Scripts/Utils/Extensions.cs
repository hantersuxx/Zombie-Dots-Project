using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static partial class Extensions
{
    public static void Log(Type type, string message)
    {
        Debug.Log($"[{type}] {DateTime.Now} DEBUG: {message}");
    }
}