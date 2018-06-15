﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static partial class Extensions
{
    public static IEnumerable<T> AddIfNotNull<T>(this IEnumerable<T> data, T value) where T : class
    {
        if (!IsNull(value))
        {
            return data.Concat(new[] { value });
        }
        return data;
    }

    private static bool IsNull<T>(T value)
    {
        return value == null || value.Equals(default(T));
    }

    public static T PickRandom<T>(this IEnumerable<T> source)
    {
        var random = new System.Random();
        return source.ElementAt(random.Next(source.Count()));
    }
}